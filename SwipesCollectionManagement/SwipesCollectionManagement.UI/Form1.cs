using SwipesCollectionManagement.Service;
using SwipesCollectionManagement.Service.Enums;
using SwipesCollectionManagement.UI.SwipesCollectingServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SwipesCollectionManagement.UI
{
    public partial class SwipesCollectionManagementForm : Form
    {
        SwipesCollectingServiceReference.SwipesCollectingServiceClient _client;
 
        List<Terminal> terminals;
        static bool flag;
        public SwipesCollectionManagementForm()
        {
            InitializeComponent();
            _client = new SwipesCollectingServiceClient();
        }



        private void EnableButton(bool isEnabled)
        {
            btnStart.Invoke((MethodInvoker)delegate
            {
                btnStart.Enabled = isEnabled;
            });
        }

        private void InitializeTerminals()
        {
            foreach (KeyValuePair<string, string> terminal in _client.GetStatus())
                terminals.Add(new Terminal(terminal.Key, dgvTerminals));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            terminals = new List<Terminal>();
            EnableButton(false);
            _client.StartCollectingSwipes();
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private bool IsProcessFinished() => terminals.Where(t => t.Status == Terminal.Statuses.Waiting || t.Status == Terminal.Statuses.InProgress).Count() == 0;

        private int GetProgress() => terminals.Where(t => t.Status == Terminal.Statuses.Finished).Count() * 100 / terminals.Count;

        private void UpdateTable()
        {
            foreach (KeyValuePair<string, string> status in _client.GetStatus())
                terminals.Where(t => t.Ip == status.Key).FirstOrDefault().UpdateTerminalStatus(status.Value, dgvTerminals);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);
            InitializeTerminals();
            flag = false;

            while (!flag)
            {
                flag = IsProcessFinished();
                UpdateTable();
                backgroundWorker1.ReportProgress(GetProgress());
                Thread.Sleep(100);
            }

            terminals.Clear();
            EnableButton(true);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgbProcess.Value = e.ProgressPercentage;
            lblProgress.Text = "Task is " + e.ProgressPercentage + "% complete";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblProgress.Text = "Done!";
        }
    }

    public class Terminal
    {
        public Terminal(string ip, DataGridView dataGridView)
        {
            Ip = ip;

            dataGridView.Invoke((MethodInvoker)delegate
            {
                dataGridView.Rows.Add(new[] { Ip, Status.ToString() });
            });

            RowCount = dataGridView.Rows.Count - 1;

            UpdateTerminalStatus("waiting", dataGridView);
        }

        public void UpdateTerminalStatus(string status, DataGridView dataGridView)
        {
            Status =
                status.ToLower().Equals("waiting") ? Statuses.Waiting :
                status.ToLower().Equals("inprogress") ? Statuses.InProgress :
                status.ToLower().Equals("finished") ? Statuses.Finished : throw new Exception($"Unknown status { status }");
            dataGridView.Invoke((MethodInvoker)delegate
            {
                switch (Status)
                {
                    case Terminal.Statuses.Waiting:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    case Terminal.Statuses.InProgress:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Yellow;
                        break;
                    case Terminal.Statuses.Finished:
                        dataGridView.Rows[RowCount].DefaultCellStyle.BackColor = Color.Green;
                        break;
                }
                dataGridView.Rows[RowCount].Cells["Status"].Value = Status.ToString();
                dataGridView.ClearSelection();
            });
        }

        public int RowCount { get; set; }
        public string Ip { get; set; }
        public Statuses Status { get; set; }

        public enum Statuses
        {
            Waiting,
            InProgress,
            Finished
        }
    }
}


