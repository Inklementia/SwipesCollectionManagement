using SwipesCollectionManagement.Service;
using SwipesCollectionManagement.Service.Enums;
using SwipesCollectionManagement.Service.Models;
using SwipesCollectionManagement.UI.SwipesCollectingServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SwipesCollectionManagement.UI
{
    public partial class SwipesCollectionManagementForm : Form
    {
        // wcf service reference
        private SwipesCollectingServiceClient _client = new SwipesCollectingServiceClient();

        //terminals for current client
        private List<TerminalModel> _terminals = new List<TerminalModel>();

        //flag to track swipes retrieval process completion
        private static bool _taskCompleted;
 
        public SwipesCollectionManagementForm()
        {
            InitializeComponent();
        }

        //enables/disables start button
        private void EnableStartButton(bool enable)
        {
            btnStart.Invoke((MethodInvoker)delegate
            {
                btnStart.Enabled = enable;
            });
        }

        //clears any dvg
        private void ClearDisplay(DataGridView dgv)
        {
            dgv.Invoke((MethodInvoker)delegate
            {
                dgv.DataSource = null;
                dgv.Columns.Clear();
            });
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            //disable button
            EnableStartButton(false);
            // clear dgv if there was smth
            ClearDisplay(dgvTerminals);
            // swipes retrieval
            _client.StartCollectingSwipes();
            _terminals = _client.GetStatus().ToList();
            // run bgworker if it is not busy
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        // returns if there are Waiting or InProcess swipes
        private bool IsTaskCompleted()
        {
            var unfinishedTerminalsCount = _terminals.Where(t => t.Status == TerminalStatus.Waiting || t.Status == TerminalStatus.InProcess).Count();
            return unfinishedTerminalsCount == 0 ? true : false;
        }

        // returns count of finished terminals
        private int FinishedTerminalsCount()
        {
            var finishedTerminalsCount = _terminals.Where(t => t.Status == TerminalStatus.Finished).Count();
            return finishedTerminalsCount;
        }

        //updates termianals dgv
        private void UpdateTerminalDataGridView()
        {
            dgvTerminals.Invoke((MethodInvoker)delegate
            {
                dgvTerminals.DataSource = _terminals;
                dgvTerminals.ClearSelection();
            });
        }

        //retrieve all swipes from the db
        private void UpdateSwipesDataGridView()
        {
            try
            {
                //retrieve all swipes from the database and display in data grid view
                var swipes = _client.GetAllSwipes().ToList();
                dgvSwipes.Invoke((MethodInvoker)delegate
                {
                    dgvSwipes.DataSource = swipes;
                    dgvSwipes.ClearSelection();
                });
            }
            catch (CommunicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);
            _taskCompleted = false;

            while (!_taskCompleted)
            {
                // getting statuses from service client
                _terminals = _client.GetStatus().ToList();
                // while there are swipes with Waiting and InProcess statuses, process retrieval is not completed
                _taskCompleted = IsTaskCompleted();
                // update dgv
                UpdateTerminalDataGridView();
                // for progress bar
                backgroundWorker1.ReportProgress(FinishedTerminalsCount() * 100 / _terminals.Count);
                //repeat every 500 ms
                Thread.Sleep(500);
            }
            // fill swipes dgv with data from db
            UpdateSwipesDataGridView();
            EnableStartButton(true);

            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //changes progressbar value and label text
            prgbProcess.Value = e.ProgressPercentage;
            lblProgress.Text = e.ProgressPercentage + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblProgress.Text = "Retrieval Completed.";
        }

        //change swipeStatus column color
        private void dgvTerminals_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                TerminalStatus status = (TerminalStatus)e.Value;
                switch (status)
                {
                    case TerminalStatus.Finished:
                        e.CellStyle.BackColor = Color.YellowGreen;
                        break;
                    case TerminalStatus.InProcess:
                        e.CellStyle.BackColor = Color.LightCoral;
                        break;
                    default:
                        e.CellStyle.BackColor = Color.LightGray;
                        break;
                }
            }
        }

        private void SwipesCollectionManagementForm_Load(object sender, EventArgs e)
        {
            ClearDisplay(dgvSwipes); // clear swipe dgv
            //_client.DeleteAllSwipes(); // clear database
            UpdateSwipesDataGridView(); // fill swipe grid with data
        }
    }



}

