using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObsidiaDebugger.Helpers;
using ObsidiaDebugger.Server;

namespace ObsidiaDebugger.Content
{
    internal partial class ObsidianDebugger : Form
    {
        private RestServer _server;

        public ObsidianDebugger ()
        {
            InitializeComponent ();
        }

        public void Update(ResponseStructure data)
        {
            Graphics graphics = CreateGraphics ();
            graphics.Clear (Color.White);

            for (int i = 0; i < data.InputsCount; i++)
            {
                Rectangle rectangle = new Rectangle (50, 50 + i*80, 30, 30);
                graphics.DrawEllipse (Pens.DarkRed, rectangle);
            }
        }

        private void Debugger_Load (object sender, EventArgs e)
        {
            _server = new RestServer (this);
        }

        private void ObsidianDebugger_Shown (object sender, EventArgs e)
        {
            Task asyncListener = new Task(AsyncListener);
            asyncListener.Start();
        }

        private void AsyncListener()
        {
            _server.StartListening ();
        }
    }
}
