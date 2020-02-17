using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace AutoGenius
{
    class Log
    {
        public static RichTextBox richTextBox;

        public static void Setup(RichTextBox formRichTextBox)
        {
            richTextBox = formRichTextBox;
        }

        public static void Info(string info)
        {
            Action action = () =>
            {
                var lines = richTextBox.Lines.ToList();
                var log = DateTime.Now.ToString("[MM-dd hh:mm:ss] ") + info;
                lines.Add(log);
                richTextBox.Lines = lines.ToArray();
            };
            richTextBox.Invoke(action);
        }
    }
}
