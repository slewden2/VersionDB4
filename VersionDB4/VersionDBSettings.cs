using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VersionDB4
{
    public class VersionDBSettings
    {
        private const string SETTINGFILE = "appsettings.json";
        private FormPositions formPositions;

        public VersionDBSettings()
        {
            formPositions = new FormPositions();
        }

        public void Load()
        {
            try
            {
                var fullPathFile = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, SETTINGFILE);
                string json = File.ReadAllText(fullPathFile);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                formPositions = jsonObj[nameof(FormPositions)]?.ToObject(typeof(FormPositions)) ?? new FormPositions();
            }
            finally
            { }
        }
        public void Save()
        {
            try
            {
                var fullPathFile = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, SETTINGFILE);
                string json = File.ReadAllText(fullPathFile);
                dynamic jsonObj = JsonConvert.DeserializeObject(json);

                jsonObj[nameof(FormPositions)] = JArray.FromObject(formPositions);

                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(fullPathFile, output);
            }
            finally
            { }
        }

        public void PositionLoad(Form frm)
        {
            var pos = formPositions.Where(x => x.Name == frm.Name).FirstOrDefault();
            if (pos == null)
            {
                return;
            }

            frm.Left = pos.Position.Left;
            frm.Top = pos.Position.Top;
            frm.Width = pos.Position.Width;
            frm.Height = pos.Position.Height;
            frm.WindowState = (FormWindowState)pos.Position.WindowState;
        }

        public void PositionSave(Form frm)
        {
            var pos = formPositions.Where(x => x.Name == frm.Name).FirstOrDefault();
            if (pos == null)
            {
                pos = new FormNamePosition()
                {
                    Name = frm.Name,
                    Position = new FormPosition()
                };
                formPositions.Add(pos);
            }

            pos.Position.Left = frm.WindowState == FormWindowState.Normal ? frm.Left : frm.RestoreBounds.Left;
            pos.Position.Top = frm.WindowState == FormWindowState.Normal ? frm.Top : frm.RestoreBounds.Top;
            pos.Position.Width = frm.WindowState == FormWindowState.Normal ? frm.Width : frm.RestoreBounds.Width;
            pos.Position.Height = frm.WindowState == FormWindowState.Normal ? frm.Height : frm.RestoreBounds.Height;
            pos.Position.WindowState = (int)(frm.WindowState == FormWindowState.Minimized ? FormWindowState.Normal : frm.WindowState);
        }
        private class FormPositions : List<FormNamePosition>
        { }
        private class FormNamePosition
        {
            public string Name { get; set; }
            public FormPosition Position { get; set; }
        }
        private class FormPosition
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int WindowState { get; set; }
        }
    }
}
