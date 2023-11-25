﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerPoint
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Model model = new Model(new Factory(new Random(Guid.NewGuid().GetHashCode())));
            PresentationModel presentationModel = new PresentationModel(model);
            Application.Run(new Form1(presentationModel, model));
        }
    }
}
