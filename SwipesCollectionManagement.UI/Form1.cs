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
        private SwipesCollectingServiceClient _client = new SwipesCollectingServiceClient();
        private static bool _taskCompleted;
      
        public SwipesCollectionManagementForm()
        {
            InitializeComponent();
          
        }
        private void EnableStartButton(bool enable)
        {
            btnStart.Invoke((MethodInvoker)delegate
            {
                btnStart.Enabled = enable;
            });
        }
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
            EnableStartButton(false);
            ClearDisplay(dgvTerminals);
            _client.StartCollectingSwipes();
         
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
      
        private bool IsTaskCompleted(List<TerminalModel> terminals)
        {
            return terminals.Where(t => t.Status == TerminalStatus.Waiting || t.Status == TerminalStatus.InProcess).Count() == 0;
        }
        private int GetProgress(List<TerminalModel> terminals)
        {
            return terminals.Where(t => t.Status == TerminalStatus.Finished).Count() * 100 / terminals.Count;
        }
        private void UpdateTerminalDataGridView(List<TerminalModel> terminals)
        {
            dgvTerminals.Invoke((MethodInvoker)delegate
            {
                dgvTerminals.DataSource = terminals;
                dgvTerminals.ClearSelection();
            });
               
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);
            _taskCompleted = false;

            while (!_taskCompleted)
            {

                List<TerminalModel> terminals = _client.GetStatus().ToList();
                backgroundWorker1.ReportProgress(GetProgress(terminals));


                _taskCompleted = IsTaskCompleted(terminals);
                UpdateTerminalDataGridView(terminals);
            
                Thread.Sleep(1000);
            }

            
            EnableStartButton(true);
            backgroundWorker1.ReportProgress(100);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgbProcess.Value = e.ProgressPercentage;
            lblProgress.Text = e.ProgressPercentage + "%";
          
        }
        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            lblProgress.Text = "Retrieval Completed.";
            UpdateSwipesDataGridView();
        }
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

        private void SwipesCollectionManagementForm_Load(object sender, EventArgs e)
        {
            ClearDisplay(dgvSwipes);
            _client.DeleteAllSwipes();
            UpdateSwipesDataGridView();
        }

     
    }


    
}


