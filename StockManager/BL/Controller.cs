using StockManager.View;
using StockManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data;

namespace StockManager.ViewModel
{


    public class MainViewModel
    {

        public static MainViewModel CurrentInstance = null;

        public MainViewModel(string Cs)
        {
            if (!string.IsNullOrEmpty(Cs))
            {
                Conectionstring = Cs;
                if (DataLayer.CheckConn(Conectionstring))
                {


                }
                else
                {
                    MessageBox.Show("DB error");
                }
            }

            CurrentInstance = this;
        }

        #region Variables
        public string Pasword = string.Empty;
        public string Username = string.Empty;


        private Dictionary<string, XControl> userControls;
        public Dictionary<string, XControl> UserControls
        {
            get
            {
                if (userControls == null)
                {
                    userControls = new Dictionary<string, XControl>();
                }
                return userControls;
            }
            set
            {
                userControls = value;
            }
        }

        System.Windows.Forms.OpenFileDialog OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        System.Windows.Forms.SaveFileDialog SaveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        #endregion

        public readonly string Conectionstring;

        #region Methods
        public void StartApplication()
        {

            InitializeControls();
            ShowWelcomeScreen();
            MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
            UcHomeScreen.ShowDialog();
        }
        private void InitializeControls()
        {
            MainWindow UcHomeScreen = new MainWindow();
            UserControls.Add(UcHomeScreen.GetType().Name, new XControl() { Control = UcHomeScreen, Previous = string.Empty });
            UcHomeScreen.BtnClickEvent += ExecuteCommand;

            UcItemMaster UcItemMaster = new UcItemMaster();
            UserControls.Add(UcItemMaster.GetType().Name, new XControl() { Control = UcItemMaster, Previous = string.Empty });
            UcItemMaster.BtnClickEvent += ExecuteCommand;

            UcProcessingScreen UcProcessingScreen = new UcProcessingScreen();
            UserControls.Add(UcProcessingScreen.GetType().Name, new XControl() { Control = UcProcessingScreen, Previous = string.Empty });
            UcProcessingScreen.BtnClickEvent += ExecuteCommand;

            UcUpdateItem UcUpdateItem = new UcUpdateItem();
            UserControls.Add(UcUpdateItem.GetType().Name, new XControl() { Control = UcUpdateItem, Previous = nameof(UcItemMaster) });
            UcUpdateItem.BtnClickEvent += ExecuteCommand;

            UcModifyExistingItem UcModifyExistingItem = new UcModifyExistingItem();
            UserControls.Add(UcModifyExistingItem.GetType().Name, new XControl() { Control = UcModifyExistingItem, Previous = nameof(UcItemMaster) });
            UcModifyExistingItem.BtnClickEvent += ExecuteCommand;

            UcChooseAction UcChooseAction = new UcChooseAction();
            UserControls.Add(UcChooseAction.GetType().Name, new XControl() { Control = UcChooseAction, Previous = string.Empty });
            UcChooseAction.BtnClickEvent += ExecuteCommand;

            UcStocks UcStocks = new UcStocks();
            UserControls.Add(UcStocks.GetType().Name, new XControl() { Control = UcStocks, Previous = nameof(UcChooseAction) });
            UcStocks.BtnClickEvent += ExecuteCommand;


            UcCreateReport UcCreateReport = new UcCreateReport();
            UserControls.Add(UcCreateReport.GetType().Name, new XControl() { Control = UcCreateReport, Previous = nameof(UcChooseAction) });
            UcCreateReport.BtnClickEvent += ExecuteCommand;

            UcReport UcReport = new UcReport();
            UserControls.Add(UcReport.GetType().Name, new XControl() { Control = UcReport, Previous = nameof(UcCreateReport) });
            UcReport.BtnClickEvent += ExecuteCommand;




        }



        public ObservableCollection<MStockitem> ClStockitem = new ObservableCollection<MStockitem>();
        public ObservableCollection<MStockitem> LoadCollectionData(bool IsForStock = false)
        {

            ClStockitem = new ObservableCollection<MStockitem>();


            if (IsForStock)
            {
                ClStockitem = DataLayer.GetDataForDetail();
            }
            else
                ClStockitem = DataLayer.GetDataForMaster();

            return ClStockitem;

        }

        public ObservableCollection<MStockitem> GetItemList()
        {
            return DataLayer.GetItemList();
        }



        private void ExecuteCommand(object sender, EventArgs e, string parentKey, string commandCode, object dataModel)
        {
            MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
            switch (commandCode)
            {


                case "Home":
                    {
                        ShowWelcomeScreen();
                    }
                    break;
                case "Back":
                    {
                        if (!string.IsNullOrEmpty(UserControls[parentKey].Previous))
                        {
                            ShowPreviousControlOnPanel(parentKey);
                        }
                    }
                    break;
                case "Next":
                    {
                        if (UcHomeScreen.CurrentPage is UcCreateReport)
                        {
                            ShowProcessingPage();
                            Thread ViewItemMaster = new Thread(delegate ()
                            {
                                UcCreateReport UcCreateReport = (UcCreateReport)UserControls[typeof(UcCreateReport).Name].Control;
                                if (UcCreateReport.ValidateModel())
                                {
                                    DataTable dt = DataLayer.GetReportData(UcCreateReport.MreportModel);

                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            UcReport UcReport = (UcReport)UserControls[typeof(UcReport).Name].Control;
                                            UcReport.LoadReport(dt, UcCreateReport.MreportModel);
                                            ShowUserControlOnPanel(UcReport);
                                        }
                                        else
                                        {
                                            MessageBox.Show("No data found in database for the applied filter");
                                            ShowParentonPanel(parentKey);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("No data found in database for the applied filter");
                                        ShowParentonPanel(parentKey);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Item must not be empty and date should be valid if used");
                                    ShowParentonPanel(parentKey);
                                }

                            });
                            ViewItemMaster.Start();
                        }

                    }
                    break;
                case "ViewItemMaster":
                    {
                        ClStockitem = new ObservableCollection<MStockitem>();
                        ShowProcessingPage();
                        Thread ViewItemMaster = new Thread(delegate ()
                        {
                            ClStockitem = LoadCollectionData();
                            UcItemMaster UcItemMaster = (UcItemMaster)UserControls[typeof(UcItemMaster).Name].Control;
                            UcItemMaster.ClStockitem = ClStockitem;
                            ShowUserControlOnPanel(UcItemMaster);
                        });
                        ViewItemMaster.Start();
                    }
                    break;
                case "ViewStock":
                    {
                        ClStockitem = new ObservableCollection<MStockitem>();
                       ShowProcessingPage();
                        Thread ViewStock = new Thread(delegate ()
                        {
                            if (GetItemList().Any())
                            {

                                ClStockitem = LoadCollectionData(true);
                                UcStocks UcStocks = UcStocks = (UcStocks)UserControls[typeof(UcStocks).Name].Control;
                                UcStocks.ClStockitem = ClStockitem;
                                ShowUserControlOnPanel(UcStocks);
                            }
                            else
                            {
                                MessageBox.Show("No item found ,please add items first");
                                ShowParentonPanel(parentKey);
                            }
                        });
                        ViewStock.Start();
                    }
                    break;
                case "ViewReport":
                    {
                        ShowProcessingPage();
                        Thread ViewReport = new Thread(delegate ()
                        {
                            if (GetItemList().Any())
                            {
                                UcCreateReport UcCreateReport = (UcCreateReport)UserControls[typeof(UcCreateReport).Name].Control;
                                UcCreateReport.MreportModel = new MreportModel();
                                ShowUserControlOnPanel(UcCreateReport);
                            }
                            else
                            {
                                MessageBox.Show("No item found ,please add items first"); ShowParentonPanel(parentKey);
                            }
                        });
                        ViewReport.Start();
                    }
                    break;
                case "ActionFromItemMaster":
                    {

                        UcItemMaster UcItemMaster = (UcItemMaster)UserControls[typeof(UcItemMaster).Name].Control;
                        if (dataModel.ToString().Equals(UcItemMaster.EAction.AddNewItem.ToString()))
                        {
                            UcUpdateItem UcUpdateItem = (UcUpdateItem)UserControls[typeof(UcUpdateItem).Name].Control;
                            UcUpdateItem.resetPage();

                            if (ClStockitem.Any())
                                UcUpdateItem.LastItemId = ClStockitem.OrderBy(i => i.ItemNo).Last().ItemNo;
                            else
                                UcUpdateItem.LastItemId = 0;
                            UcUpdateItem.ClStockitem = ClStockitem;
                            ShowUserControlOnPanel(UcUpdateItem);
                        }

                        else if (dataModel.ToString().Equals(UcItemMaster.EAction.UpdateExistingitem.ToString()))
                        {

                            if (ClStockitem.Any())
                            {
                                UcModifyExistingItem UcModifyExistingItem = (UcModifyExistingItem)UserControls[typeof(UcModifyExistingItem).Name].Control;
                                UcModifyExistingItem.CurrentModel = null;

                                UcModifyExistingItem.ClStockitem = ClStockitem;
                                ShowUserControlOnPanel(UcModifyExistingItem);
                            }
                            else
                            {
                                MessageBox.Show("Please add item first");

                            }
                        }


                    }
                    break;
                case "AddNewItem":
                    {
                        UcUpdateItem UcUpdateItem = (UcUpdateItem)UserControls[typeof(UcUpdateItem).Name].Control;

                        ShowProcessingPage();
                        Thread THAddNewItem = new Thread(delegate ()
                        {
                            if (DataLayer.InsertItemIntoMaster(UcUpdateItem.CreateNewModel()))
                            {

                               // ClStockitem.Add(UcUpdateItem.CreateNewModel());

                                UcItemMaster UcItemMaster = (UcItemMaster)UserControls[typeof(UcItemMaster).Name].Control;
                                UcItemMaster.ClStockitem = LoadCollectionData();
                                ShowUserControlOnPanel(UcItemMaster);
                            }
                            else
                            {
                                MessageBox.Show("Adding Item Failed"); ShowParentonPanel(parentKey);
                                ShowParentonPanel(parentKey);
                            }
                        });
                        THAddNewItem.Start();

                    }
                    break;
                case "UpdateExistingItem":
                    {
                        UcModifyExistingItem UcModifyExistingItem = (UcModifyExistingItem)UserControls[typeof(UcModifyExistingItem).Name].Control;

                        ShowProcessingPage();
                        Thread UpdateExistingItem = new Thread(delegate ()
                        {
                            if (DataLayer.UpdateItemInMaster(UcModifyExistingItem.CurrentModel))
                            {
                                ClStockitem = LoadCollectionData();
                                UcItemMaster UcItemMaster = (UcItemMaster)UserControls[typeof(UcItemMaster).Name].Control;
                                UcItemMaster.ClStockitem = ClStockitem;
                                ShowUserControlOnPanel(UcItemMaster);
                            }
                            else
                            {
                                MessageBox.Show("Update Item Failed");
                                ShowParentonPanel(parentKey);
                            }

                        });
                        UpdateExistingItem.Start();
                    }
                    break;
                case "DeleteExistingItem":
                    {
                        UcModifyExistingItem UcModifyExistingItem = (UcModifyExistingItem)UserControls[typeof(UcModifyExistingItem).Name].Control;
                        ShowProcessingPage();
                        Thread DeleteExistingItem = new Thread(delegate ()
                        {
                            if (DataLayer.DeleteItemFromMaster(UcModifyExistingItem.CurrentModel))
                            {

                                ClStockitem = LoadCollectionData();
                                UcItemMaster UcItemMaster = (UcItemMaster)UserControls[typeof(UcItemMaster).Name].Control;
                                UcItemMaster.ClStockitem = ClStockitem;
                                ShowUserControlOnPanel(UcItemMaster);
                            }
                            else
                            {
                                MessageBox.Show("Delete Item Failed");
                                ShowParentonPanel(parentKey);

                            }
                        });
                        DeleteExistingItem.Start();

                    }
                    break;

                case "UpdateStock":
                    {

                        ShowProcessingPage();
                        Thread UpdateStock = new Thread(delegate ()
                        {
                            var ClstockToBeUpdated = ((ObservableCollection<MStockitem>)dataModel).Where(i => i.IsChanged).ToList();

                            var LastItem = ClstockToBeUpdated.Last();

                            foreach (var RequestModel in ClstockToBeUpdated)
                            {
                                if (DataLayer.InsertItemIntoDetail(RequestModel))
                                {
                                    if (RequestModel.Equals(LastItem))
                                    {
                                        UcStocks UcStocks = (UcStocks)UserControls[typeof(UcStocks).Name].Control;
                                        UcStocks.ClStockitem = LoadCollectionData(true);
                                        ShowUserControlOnPanel(UcStocks);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Action Failed");
                                    ShowParentonPanel(parentKey);
                                    break;
                                }
                            }
                        });
                        UpdateStock.Start();
                    }
                    break;
                default:
                    break;
            }
        }
        private void ShowUserControlOnPanel(System.Windows.Controls.Control Control)
        {
            try
            {
                MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
                UcHomeScreen.ShowPage(Control);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void ShowPreviousControlOnPanel(string parentKey)
        {
            try
            {
                System.Windows.Controls.Control control = UserControls[UserControls[parentKey].Previous].Control;
                MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
                UcHomeScreen.ShowPage(control);
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void ShowNextControlOnPanel(string parentKey)
        {
            try
            {
                System.Windows.Controls.Control control = UserControls[UserControls[parentKey].Next].Control;
                MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
                UcHomeScreen.ShowPage(control);
            }
            catch (Exception)
            {
                throw;
            }
        }

        void ShowParentonPanel(string parentKey)
        {
            try
            {
                if (parentKey != null && !string.IsNullOrEmpty(parentKey))
                {
                    System.Windows.Controls.Control control = UserControls[parentKey].Control;
                    MainWindow homeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
                    homeScreen.ShowPage(control);
                }


            }
            catch (Exception)
            {
               
            }
        }
        private void ShowProcessingPage(string Tempmsg = "")
        {
            try
            {
                UcProcessingScreen uc = (UcProcessingScreen)UserControls[typeof(UcProcessingScreen).Name].Control;
                uc.ProcessingMessage = Tempmsg;
                MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;
                UcHomeScreen.ShowPage(uc, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ShowWelcomeScreen()
        {
            MainWindow UcHomeScreen = (MainWindow)UserControls[typeof(MainWindow).Name].Control;

            ShowUserControlOnPanel((UcChooseAction)UserControls[typeof(UcChooseAction).Name].Control);
        }
        #endregion

    }
}
