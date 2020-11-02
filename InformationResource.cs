using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISAS.Inventory
{
    public class InformationResource
    {
        private string title;
        private string  description;
        private int count;
        bool personaldata;
        bool commercial_data;
        bool statesecret;
        int valueres;
        int critlvl;
        int category;
        double threatsLvl;
        double risk;
        public string Title { get => title; set => title = value; }
        public int Count { get => count; set => count = value; }
        public string Description { get => description; set => description = value; }
        public bool Personaldata { get => personaldata; set => personaldata = value; }
        public bool Commercial_data { get => commercial_data; set => commercial_data = value; }
        public bool Statesecret { get => statesecret; set => statesecret = value; }
        public int Valueres { get => valueres; set => valueres = value; }
        public int Critlvl { get => critlvl; set => critlvl = value; }
        public int Category { get => category; set => category = value; }
        public double ThreatsLvl { get => threatsLvl; set => threatsLvl = value; }
        public double Risk { get => risk; set => risk = value; }

        public InformationResource(string title, int count = 1, string desc = "", bool personal_data = false, bool commercial_data = false,
            bool state_secret = false, int valueres = 0, int critlvl = 0, int category = 0, double threatsLvl = 0, double risk=0)
        {
            this.Title = title;
            this.Count = count;
            this.Description = desc;
            this.Personaldata = personal_data;
            this.Commercial_data = commercial_data;
            this.Statesecret = state_secret;
            this.Valueres = valueres;
            this.Critlvl = critlvl;
            this.Category = category;
            this.ThreatsLvl = threatsLvl;
            this.Risk = risk;
        }

        public void WriteToXml(string filename)
        {
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
            XElement root = xdoc.Element("InformationResources");
            // добавляем новый элемент
            root.Add(new XElement("InformationResource",
                        new XAttribute("title", this.Title),
                        new XElement("count", this.Count),
                        new XElement("description", this.Description),
                        new XElement("Personaldata", this.Personaldata),
                        new XElement("Commercial_data", this.Commercial_data),
                        new XElement("Statesecret", this.Statesecret),
                        new XElement("Valueres", this.Valueres),
                        new XElement("Critlvl", this.Critlvl),
                        new XElement("Category", this.Category)
                        ));
            xdoc.Save(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
        }
        public static List<InformationResource> ReadFromXml(string filename)
        {
            List<InformationResource> result = new List<InformationResource>();
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\"+filename));
            XElement root = xdoc.Element("InformationResources");
            foreach (XElement resourceElement in root.Elements("InformationResource"))
            {
                string desc;
                bool pd, cd, sts;
                int val, crit, cat;
                double risk, threatsLvl;
                XAttribute titleAttribute = resourceElement.Attribute("title");
                XElement countElement = resourceElement.Element("count");
                try { desc = resourceElement.Element("description").Value;}
                catch { desc = ""; }
                try { pd = bool.Parse(resourceElement.Element("Personaldata").Value); }
                catch { pd = false; }
                try { cd = bool.Parse(resourceElement.Element("Commercial_data").Value); }
                catch { cd = false; }
                try { sts = bool.Parse(resourceElement.Element("Statesecret").Value); }
                catch { sts = false; }
                try { val = int.Parse(resourceElement.Element("Valueres").Value); }
                catch { val = 0; }
                try { crit = int.Parse(resourceElement.Element("Critlvl").Value); }
                catch { crit = 0; }
                try { cat = int.Parse(resourceElement.Element("Category").Value); }
                catch { cat = 0; }
                try { risk = double.Parse(resourceElement.Element("Risk").Value.Replace('.', ',')); }
                catch { risk = 0; }
                try { threatsLvl = double.Parse(resourceElement.Element("ThreatsLvl").Value.Replace('.', ',')); }
                catch { threatsLvl = 0; }
                result.Add(new InformationResource(titleAttribute.Value, int.Parse(countElement.Value),
                    desc, pd, cd, sts, val, crit, cat, threatsLvl, risk)) ;
            }
            return result;
        }

        public void DeleteFromXml(string filename, string title)
        {
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
            XElement root = xdoc.Element("InformationResources");
            foreach (XElement resourceElement in root.Elements("InformationResource"))
            {
                if (resourceElement.Attribute("title").Value == title)
                    resourceElement.Remove();
            }
            xdoc.Save(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
        }
        public void EditXml(string filename)
        {
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
            XElement root = xdoc.Element("InformationResources");
            foreach (XElement resourceElement in root.Elements("InformationResource"))
            {
                if (resourceElement.Attribute("title").Value == this.Title)
                {
                    resourceElement.Element("count").SetValue( count.ToString());
                    try { resourceElement.Element("description").SetValue(this.Description.ToString()); }
                    catch { resourceElement.Add(new XElement("description", this.Description)); }
                    try { resourceElement.Element("Personaldata").SetValue(this.Personaldata); }
                    catch { resourceElement.Add(new XElement("Personaldata", this.Personaldata)); }
                    try { resourceElement.Element("Commercial_data").SetValue(this.Commercial_data); }
                    catch { resourceElement.Add(new XElement("Commercial_data", this.Commercial_data)); }
                    try { resourceElement.Element("Statesecret").SetValue(this.Statesecret); }
                    catch { resourceElement.Add(new XElement("Statesecret", this.Statesecret)); }
                    try { resourceElement.Element("Valueres").SetValue(this.Valueres); }
                    catch { resourceElement.Add(new XElement("Valueres", this.Valueres)); }
                    try { resourceElement.Element("Critlvl").SetValue(this.Critlvl); }
                    catch { resourceElement.Add(new XElement("Critlvl", this.Critlvl)); }
                    try { resourceElement.Element("Category").SetValue(this.Category); }
                    catch { resourceElement.Add(new XElement("Category", this.Category)); }
                    try { resourceElement.Element("ThreatsLvl").SetValue(this.ThreatsLvl); }
                    catch { resourceElement.Add(new XElement("ThreatsLvl", this.ThreatsLvl)); }
                    try { resourceElement.Element("Risk").SetValue(this.Risk); }
                    catch { resourceElement.Add(new XElement("Risk", this.Risk)); }
                }
            }
            xdoc.Save(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
        }

        public static InformationResource FindResourceXml(string filename, string title)
        {
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
            XElement root = xdoc.Element("InformationResources");
            foreach (XElement resourceElement in root.Elements("InformationResource"))
            {
                if (resourceElement.Attribute("title").Value == title)
                {
                    int count = int.Parse(resourceElement.Element("count").Value);
                    string desc;
                    bool pd, cd, sts;
                    int val, crit, cat;
                    double risk, threatsLvl;
                    try { desc = resourceElement.Element("description").Value; }
                    catch { desc = ""; }
                    try { pd = bool.Parse(resourceElement.Element("Personaldata").Value); }
                    catch { pd = false; }
                    try { cd = bool.Parse(resourceElement.Element("Commercial_data").Value); }
                    catch { cd = false; }
                    try { sts = bool.Parse(resourceElement.Element("Statesecret").Value); }
                    catch { sts = false; }
                    try { val = int.Parse(resourceElement.Element("Valueres").Value); }
                    catch { val = 0; }
                    try { crit = int.Parse(resourceElement.Element("Critlvl").Value); }
                    catch { crit = 0; }
                    try { cat = int.Parse(resourceElement.Element("Category").Value); }
                    catch { cat = 0; }
                    try { risk = double.Parse(resourceElement.Element("Risk").Value.Replace('.', ',')); }
                    catch { risk = 0; }
                    try { threatsLvl = double.Parse(resourceElement.Element("ThreatsLvl").Value.Replace('.', ',')); }
                    catch { threatsLvl = 0; }
                    return new InformationResource(title, count, desc, pd, cd, sts, val,crit, cat, threatsLvl,risk);
                }
            }
            return null;
        }


        public static void DeleteAllFromXml(string filename)
        {
            XDocument xdoc = XDocument.Load(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
            XElement root = xdoc.Element("InformationResources");
            root.RemoveAll();
            xdoc.Save(Environment.CurrentDirectory.Replace("bin\\Debug", "Inventory\\lists\\" + filename));
        }
    }
}
