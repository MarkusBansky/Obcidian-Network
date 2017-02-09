using System.Threading.Tasks;
using System.Windows.Forms;
using ObsidiaDebugger.Content;

namespace ObsidiaDebugger
{
    public class NeuralNetworkDebugger
    {
        public NeuralNetworkDebugger()
        {
            Task asyncForm = new Task(AsyncForm);
            asyncForm.Start();
        }

        private void AsyncForm()
        {
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault (false);
            Application.Run (new ObsidianDebugger ());
        }
    }
}