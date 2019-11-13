using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEReader
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var fn = Path.Combine($@"d:\helloworld.exe");
            var fileBytes = File.ReadAllBytes(fn);


            IMAGE_DOS_HEADER dosHeader = new IMAGE_DOS_HEADER();
            dosHeader = Utils.BytesToStruct<IMAGE_DOS_HEADER>(fileBytes, 0, Utils.StructSize(dosHeader));
            var bytes = Utils.StructToBytes(dosHeader, Utils.StructSize(dosHeader));
            textBox1.Text = Utils.BytesToHexString(bytes);

            IMAGE_SECTION_HEADER aa;

        }


    }
}
