using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Common;
using DAL;
using LSExtensionWindowLib;
using LSSERVICEPROVIDERLib;
using MSXML;
//using Oracle.DataAccess.Client;

namespace NumerousResultEntry
{
    [ComVisible(true)]
    [ProgId("NumerousResultEntry.NumerousResultEntrycls")]
    public partial class NumerousResultEntry : UserControl, IExtensionWindow
    {
        #region Ctor

        public NumerousResultEntry()
        {
            InitializeComponent();
            BackColor = Color.FromName("Control");
        }

        #endregion

        #region Private members

        //עילם
        private string _barcodeField;
        private string valueParm;
        private string _displayFields;

        private Dictionary<string, string> _entityIcons;

        private INautilusDBConnection _ntlsCon;
        private IExtensionWindowSite2 _ntlsSite;
        private INautilusProcessXML _processXml;

        //עילם
        private INautilusServiceProvider _sp;

        private string _tableName;

        private string _titleName;

        private IDataLayer dal;

        #endregion

        #region Implementing IExtensionWindow

        public void PreDisplay()
        {
           
            Utils.CreateConstring(_ntlsCon);
            dal = new DataLayer();
            dal.Connect();
            timerFocus.Start();

        }

        public void SetParameters(string parameters)
        {
            if (listViewEntities.Columns.Count <= 0) //first time
            {
                _tableName = ""; // splitedParameters[index++];
                _barcodeField = "result name"; // splitedParameters[index++];
                _displayFields = ""; // splitedParameters[index++];

                valueParm = parameters;
                _titleName = valueParm + " הזנת תוצאות "; // splitedParameters[index++];
                InitControls();
                LoadPictures();
            }
        }

        public bool CloseQuery()
        {
            return true;
        }

        public void Internationalise()
        {
        }

        public void SetSite(object site)
        {
            _ntlsSite = (IExtensionWindowSite2)site;
            _ntlsSite.SetWindowInternalName("TEST");
            _ntlsSite.SetWindowRegistryName("TEST");
            _ntlsSite.SetWindowTitle("Fire Event");
        }

        public WindowButtonsType GetButtons()
        {
            return WindowButtonsType.windowButtonsNone;
        }

        public bool SaveData()
        {
            return false;
        }

        public void SaveSettings(int hKey)
        {
        }

        public void Setup()
        {
        }

        public void refresh()
        {
        }

        public WindowRefreshType DataChange()
        {
            return WindowRefreshType.windowRefreshNone;
        }

        public WindowRefreshType ViewRefresh()
        {
            return WindowRefreshType.windowRefreshNone;
        }

        public void SetServiceProvider(object serviceProvider)
        {
            _sp = serviceProvider as NautilusServiceProvider;
            _processXml = Utils.GetXmlProcessor(_sp); //.QueryServiceProvider("ProcessXML") as NautilusProcessXML;
            _ntlsCon = Utils.GetNtlsCon(_sp);
        }

        public void RestoreSettings(int hKey)
        {
        }



        #endregion

        #region Events

        public int imageIndex;

        private void txtEditEntity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                

                if (e.KeyChar == (char)13 && txtEditEntity.Text != "") //Enter
                {
                    string value = txtEditEntity.Text.Trim();
                    int resultId;
                    //Checks if it's already in list view
                    if (!ListViewContains(txtEditEntity.Text.Trim()))
                    {
                        if ((value.StartsWith("R", StringComparison.OrdinalIgnoreCase) || value.StartsWith("ר"))
                        && int.TryParse(value.Replace(value[0], ' ').Trim(), out resultId))
                        {
                            Result currentResult = dal.IsGoodResultForEntry(resultId, 'C', 'V', 'U', 'S', 'P');
                            if (currentResult != null)
                            {
                                ListViewItem li = null;
                                li = new ListViewItem(currentResult.Name, imageIndex++);
                                li.SubItems.Add(currentResult.ResultId.ToString());
                                li.SubItems.Add(currentResult.Test.Aliquot.Sample.Name);
                                li.SubItems.Add(valueParm);
                                li.SubItems.Add(currentResult.Test.TEST_ID.ToString());
                                listViewEntities.Items.Add(li);
                                close_button.Enabled = false;
                                txtEnterdEntity.Text = txtEditEntity.Text;
                                txtEditEntity.Text = string.Empty;
                            }
                            else
                            {
                                BadValue("הבדיקה אינה קיימת במערכת");
                            }
                        }
                        else
                        {
                            BadValue("הבדיקה אינה בפורמט המתאים");
                        }
                    }
                    else
                    {
                        BadValue("הבדיקה כבר נמצאת ברשימה");
                    }
                    if (e.KeyChar == (char)13) e.Handled = true;
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLogFile(ex);
                MessageBox.Show(ex.Message + "אנא פנה לתמיכה ");
            }
        }

        private void BadValue(string senderTxtResult)
        {

            for (int i = 0; i < 4; i++)
            {
                SystemSounds.Asterisk.Play();
                Thread.Sleep(200);

            }

            MessageBox.Show(senderTxtResult);
            txtEditEntity.Focus();
            txtEditEntity.Clear();

        }

        private void listViewEntities_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) //Delete
            {
                //Remove all selected rows
                foreach (ListViewItem item in listViewEntities.SelectedItems)
                {
                    listViewEntities.Items.Remove(item);
                }
            }
        }


        private bool ListViewContains(string toChek)
        {
            toChek = toChek.Remove(0, 1);
            foreach (ListViewItem item in listViewEntities.Items)
            {
                if (item.SubItems[1].Text == toChek)
                    return true;
            }
            return false;
        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
        
            ResultEntry();

            //Empties the list
            foreach (ListViewItem item in listViewEntities.Items)
            {
                listViewEntities.Items.Remove(item);
            }
            close_button.Enabled = true;
            txtEditEntity.Focus();
        }

        private void Close_button_Click(object sender, EventArgs e)
        {
            if (listViewEntities.Items.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("האם אתה בטוח שברצונך לצאת ממסך זה ללא אישור? ", "יציאה",
                                                            MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    listViewEntities = null;
                    _ntlsSite.CloseWindow();
                }
            }
            else
            {
                listViewEntities = null;
                _ntlsSite.CloseWindow();
            }
        }

        #endregion

        #region Private methods

        private void InitControls()
        {
            //Set title
            lblTitle.Text = _titleName;
            // Add barcodeField column
            listViewEntities.Columns.Add(_barcodeField, _barcodeField, 150, HorizontalAlignment.Left, 0);
            //Add other columns
            _displayFields = "result id,sample name,result,Test id";
            string[] columns = _displayFields.Split(',');
            foreach (string item in columns)
            {
                listViewEntities.Columns.Add(item, item, 150, HorizontalAlignment.Left, 0);
            }
        }

        private void LoadPictures()
        {
            //string path = "E://Program Files//Thermo//Nautilus//Resource//";

            //_entityIcons = new Dictionary<string, string>();
            //_entityIcons.Add("not status", _tableName + ".ico");
            //_entityIcons.Add("A", path + _tableName + "a" + ".ico");
            //_entityIcons.Add("C", path + _tableName + "c" + ".ico");
            //_entityIcons.Add("P", path + _tableName + "p" + ".ico");
            //_entityIcons.Add("I", path + _tableName + "i" + ".ico");
            //_entityIcons.Add("R", path + _tableName + "r" + ".ico");
            //_entityIcons.Add("S", path + _tableName + "s" + ".ico");
            //_entityIcons.Add("U", path + _tableName + "u" + ".ico");
            //_entityIcons.Add("V", path + _tableName + "v" + ".ico");
            //_entityIcons.Add("X", path + _tableName + "x" + ".ico");
        }

        private IXMLDOMElement ObjResultEntryElem(DOMDocument objDom, string value, string resultId)
        {
            IXMLDOMElement objResultEntryElem = objDom.createElement("result-entry");
            objResultEntryElem.setAttribute("result-id", resultId);
            objResultEntryElem.setAttribute("original-result", value);
            return objResultEntryElem;
        }

        private void ResultEntry()
        {
            var objDom = new DOMDocument();
            IXMLDOMElement objResultRequest = objDom.createElement("lims-request");
            objDom.appendChild(objResultRequest);
            foreach (ListViewItem item in listViewEntities.Items)
            {
                IXMLDOMElement objResultRequest2 = objDom.createElement("result-request");
                //gets entity name
                string resultId = item.SubItems[1].Text;
                string testId = item.SubItems[4].Text;

                IXMLDOMElement objLoad = objDom.createElement("load");
                objLoad.setAttribute("entity", "TEST");

                objLoad.setAttribute("id", testId);
                objLoad.setAttribute("mode", "entry");

                objResultRequest2.appendChild(objLoad);
                objResultRequest.appendChild(objResultRequest2);

                IXMLDOMElement objResultEntryElem = ObjResultEntryElem(objDom, valueParm, resultId);
                objLoad.appendChild(objResultEntryElem);
            }
            var res = new DOMDocument();
            _processXml.ProcessXMLWithResponse(objDom, res);
            // For testing
            // objDom.save(@"C:\temp\docResultEntry.xml");
            //res.save(@"C:\temp\resResultEntry.xml");
        }

        #endregion

        private void timerFocus_Tick(object sender, EventArgs e)
        {
            txtEditEntity.Focus();
            timerFocus.Stop();

        }

        private void NumerousResultEntry_Resize(object sender, EventArgs e)
        {
            panel2.Location = new Point(Width / 2 - panel2.Width / 2, panel2.Location.Y);
        }

   


  

   
    }
}