using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TemaHotel.DataAccess;
using TemaHotel.Model;
using TemaHotel.Utilities;

namespace TemaHotel.ViewModel
{
    public class ManageVm : INotifyPropertyChanged
    {
        ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();
        ObservableCollection<ExtraServices> extraSvs = new ObservableCollection<ExtraServices>();
        ObservableCollection<Deal> deals = new ObservableCollection<Deal>();
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private string extraSvName;
        private double extraSvPrice;

        private string dealName;
        private int nrNights;
        private int dealPrice;
        private DateTime dealStart = DateTime.Now;
        private DateTime dealEnd = DateTime.Now;

        private bool showUpdateDetails;//Show facility tab menu update buttons
        private bool showExtraSvUpdateDetails; //show extra services tab menu update buttons
        private bool showDealsDetails; // show deals tab menu update buttons

        //Commands for Tab Facility
        private ICommand createFacilityCommand;
        private ICommand deleteFacilityCommand;
        private ICommand updateFacilityCommand;
        private ICommand clearDataControlsCommand;
        private ICommand saveUpdateModificationCommand;
        private ICommand cancelUpdateStateCommand;

        //Commands for Tab Extra Services
        private ICommand createDealCommand;
        private ICommand deleteDealCommand;
        private ICommand updateDealCommand;
        private ICommand clearDealControlsCommand;
        private ICommand saveDealChangesCommand;
        private ICommand cancelDealChangesCommand;

        //Commands for Tab Deals
        private ICommand createExtraSvCommand;
        private ICommand deleteExtraSvCommand;
        private ICommand updateExtraSvCommand;
        private ICommand clearExtraSvControlsCommand;
        private ICommand saveExtraSvChangesCommand;
        private ICommand cancelExtraSvChangesCommand;

        public ManageVm()
        {
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.GetFacilities().ForEach(Facilities.Add);

            ExtraSvServiceLayer extraSvL = new ExtraSvServiceLayer();
            extraSvL.GetServices().ForEach(ExtraSvs.Add);

            DealServiceLayer dealSv = new DealServiceLayer();
            dealSv.GetDeals().ForEach(Deals.Add);
        }

        public bool ShowUpdateDetails
        {
            get { return showUpdateDetails; }
            set
            {
                showUpdateDetails = value;
                OnPropertyChanged("ShowUpdateDetails");
            }
        }

        public bool ShowExtraSvUpdateDetails
        {
            get { return showExtraSvUpdateDetails; }
            set
            {
                showExtraSvUpdateDetails = value;
                OnPropertyChanged("ShowExtraSvUpdateDetails");
            }
        }

        public bool ShowDealsDetails
        {
            get { return showDealsDetails; }
            set
            {
                showDealsDetails = value;
                OnPropertyChanged("ShowDealsDetails");
            }
        }

        //Facility Tab Icommand Properties
        public ICommand CreateFacilityCommand
        {
            get
            {
                if (createFacilityCommand == null)
                {
                    createFacilityCommand = new RelayCommand(CreateFacility);
                }
                return createFacilityCommand;
            }
        }

        public ICommand DeleteFacilityCommand
        {
            get
            {
                if (deleteFacilityCommand == null)
                {
                    deleteFacilityCommand = new RelayCommand(DeleteFacility);
                }
                return deleteFacilityCommand;
            }
        }

        public ICommand UpdateFacilityCommand
        {
            get
            {
                if (updateFacilityCommand == null)
                {
                    updateFacilityCommand = new RelayCommand(UpdateFacility);
                }
                return updateFacilityCommand;
            }
        }

        public ICommand ClearDataControlsCommand
        {
            get
            {
                if (clearDataControlsCommand == null)
                {
                    clearDataControlsCommand = new RelayCommand(ClearDataControls);
                }
                return clearDataControlsCommand;
            }
        }

        public ICommand SaveUpdateModificationCommand
        {
            get
            {
                if (saveUpdateModificationCommand == null)
                {
                    saveUpdateModificationCommand = new RelayCommand(SaveUpdateModification);
                }
                return saveUpdateModificationCommand;
            }
        }

        public ICommand CancelUpdateStateCommand
        {
            get
            {
                if (cancelUpdateStateCommand == null)
                {
                    cancelUpdateStateCommand = new RelayCommand(CancelUpdateState);
                }
                return cancelUpdateStateCommand;
            }
        }


        //Extra Services Tab Icommand Properties
        public ICommand CreateExtraSvCommand
        {
            get
            {
                if (createExtraSvCommand == null)
                {
                    createExtraSvCommand = new RelayCommand(CreateExtraSv);
                }
                return createExtraSvCommand;
            }
        }

        public ICommand DeleteExtraSvCommand
        {
            get
            {
                if (deleteExtraSvCommand == null)
                {
                    deleteExtraSvCommand = new RelayCommand(DeleteExtraSv);
                }
                return deleteExtraSvCommand;
            }
        }

        public ICommand UpdateExtraSvCommand
        {
            get
            {
                if (updateExtraSvCommand == null)
                {
                    updateExtraSvCommand = new RelayCommand(UpdateExtraSv);
                }
                return updateExtraSvCommand;
            }
        }

        public ICommand ClearExtraSvControlsCommand
        {
            get
            {
                if (clearExtraSvControlsCommand == null)
                {
                    clearExtraSvControlsCommand = new RelayCommand(ClearExtraSvControls);
                }
                return clearExtraSvControlsCommand;
            }
        }

        public ICommand SaveExtraSvChangesCommand
        {
            get
            {
                if (saveExtraSvChangesCommand == null)
                {
                    saveExtraSvChangesCommand = new RelayCommand(SaveExtraSvChanges);
                }
                return saveExtraSvChangesCommand;
            }
        }

        public ICommand CancelExtraSvChangesCommand
        {
            get
            {
                if (cancelExtraSvChangesCommand == null)
                {
                    cancelExtraSvChangesCommand = new RelayCommand(CancelExtraSvState);
                }
                return cancelExtraSvChangesCommand;
            }
        }


        //Deals Tab Icommand Properties
        public ICommand CreateDealCommand
        {
            get
            {
                if (createDealCommand == null)
                {
                    createDealCommand = new RelayCommand(CreateDeal);
                }
                return createDealCommand;
            }
        }

        public ICommand DeleteDealCommand
        {
            get
            {
                if (deleteDealCommand == null)
                {
                    deleteDealCommand = new RelayCommand(DeleteDeal);
                }
                return deleteDealCommand;

            }
        }

        public ICommand UpdateDealCommand
        {
            get
            {
                if (updateDealCommand == null)
                {
                    updateDealCommand = new RelayCommand(UpdateDeal);
                }
                return updateDealCommand;
            }
        }

        public ICommand ClearDealControlsCommand
        {
            get
            {
                if (clearDealControlsCommand == null)
                {
                    clearDealControlsCommand = new RelayCommand(ClearDealControls);
                }
                return clearDealControlsCommand;
            }
        }

        public ICommand SaveDealChangesCommand
        {
            get
            {
                if (saveDealChangesCommand == null)
                {
                    saveDealChangesCommand = new RelayCommand(SaveDealChanges);
                }
                return saveDealChangesCommand;

            }
        }

        public ICommand CancelDealChangesCommand
        {
            get
            {
                if (cancelDealChangesCommand == null)
                {
                    cancelDealChangesCommand = new RelayCommand(CancelDealChanges);
                }
                return cancelDealChangesCommand;
            }
        }


        public ObservableCollection<Facility> Facilities
        {
            get { return facilities; }
            set
            {
                facilities = value;
                OnPropertyChanged("Facilities");
            }
        }

        public ObservableCollection<ExtraServices> ExtraSvs
        {
            get { return extraSvs; }
            set
            {
                extraSvs = value;
                OnPropertyChanged("ExtraSvs");
            }
        }

        public ObservableCollection<Deal> Deals
        {
            get { return deals; }
            set
            {
                deals = value;
                OnPropertyChanged("Deals");
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string ExtraSvName
        {
            get { return extraSvName; }
            set
            {
                extraSvName = value;
                OnPropertyChanged("ExtraSvName");
            }
        }

        public double ExtraSvPrice
        {
            get { return extraSvPrice; }
            set
            {
                extraSvPrice = value;
                OnPropertyChanged("ExtraSvPrice");
            }
        }

        public string DealName
        {
            get { return dealName; }
            set
            {
                dealName = value;
                OnPropertyChanged("DealName");
            }
        }

        public int NrNights
        {
            get { return nrNights; }
            set
            {
                nrNights = value;
                OnPropertyChanged("NrNights");
            }
        }

        public int DealPrice
        {
            get { return dealPrice; }
            set
            {
                dealPrice= value;
                OnPropertyChanged("DealPrice");
            }
        }

        public DateTime DealStart
        {
            get { return dealStart; }
            set
            {
                dealStart = value;
                OnPropertyChanged("DealStart");
            }
        }

        public DateTime DealEnd
        {
            get { return dealEnd; }
            set
            {
                dealEnd = value;
                OnPropertyChanged("DealEnd");
            }
        }


        //Manage FAcilitites Methods
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void CreateFacility(object param)
        {
            if (Name != null && Name.Equals("") == false)
            {
                FacilityServiceLayer facSv = new FacilityServiceLayer();
                var item = Facilities.Where(p => p.Name.Equals(Name));
                List<Facility> facil = item.ToList<Facility>();
                if (facil.Count > 0)
                {
                    MessageBox.Show("This facility is already in database");
                    return;
                }
                Facility fac = new Facility(Name);
                facSv.AddFacility(fac);
                Facilities.Add(fac);
                Name = null;
                MessageBox.Show("Facility created");
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        public void DeleteFacility(object param)
        {
            Facility us = param as Facility;
            FacilityServiceLayer sv = new FacilityServiceLayer();
            sv.DeleteFacility(us);
            Facilities.Remove(us);
        }

        public void UpdateFacility(object param)
        {
            Facility facToUp = param as Facility;
            if (facToUp != null)
            {
                Name = facToUp.Name;
                ShowUpdateDetails = true;
            }
        }

        public void ClearDataControls(object param)
        {
            Name = null;
        }

        public void SaveUpdateModification(object param)
        {
            ShowUpdateDetails = false;
            FacilityServiceLayer facSv = new FacilityServiceLayer();
            Facility us = param as Facility;
            for (int i = 0; i < Facilities.Count; i++)
            {
                if (Facilities[i].Id == us.Id)
                {
                    facilities[i].Name = Name;
                    facSv.ModifyFacility(facilities[i]);
                    Facilities.Clear();
                    facSv.GetFacilities().ForEach(Facilities.Add);
                    Name = null;
                    break;
                }
            }
        }

        public void CancelUpdateState(object param)
        {
            ShowUpdateDetails = false;
            Name = null;
        }

        //Manage Extra Services Methods
        public void CreateExtraSv(object param)
        {
            if (ExtraSvName != null && ExtraSvName.Equals("") == false && ExtraSvPrice > 0)
            {
                ExtraSvServiceLayer exSv = new ExtraSvServiceLayer();
                var item = ExtraSvs.Where(p => (p.Name.Equals(ExtraSvName) && p.Price == ExtraSvPrice));
                List<ExtraServices> facil = item.ToList<ExtraServices>();
                if (facil.Count > 0)
                {
                    MessageBox.Show("This Service is already in database");
                    return;
                }
                ExtraServices ex = new ExtraServices(ExtraSvName, ExtraSvPrice);
                exSv.AddExtraSvIntoDb(ex);
                ExtraSvs.Add(ex);
                ExtraSvName = null;
                ExtraSvPrice = 0;
                MessageBox.Show("Service created");
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        public void DeleteExtraSv(object param)
        {
            ExtraServices us = param as ExtraServices;
            ExtraSvServiceLayer sv = new ExtraSvServiceLayer();
            sv.DeleteService(us);
            ExtraSvs.Remove(us);
        }

        public void UpdateExtraSv(object param)
        {
            ExtraServices exToUp = param as ExtraServices;
            if (exToUp != null)
            {
                ExtraSvName = exToUp.Name;
                ExtraSvPrice = exToUp.Price;
                ShowExtraSvUpdateDetails = true;
            }
        }

        public void ClearExtraSvControls(object param)
        {
            ExtraSvName = null;
            ExtraSvPrice = 0;
        }

        public void SaveExtraSvChanges(object param)
        {
            ShowExtraSvUpdateDetails = false;
            ExtraSvServiceLayer facSv = new ExtraSvServiceLayer();
            ExtraServices us = param as ExtraServices;
            for (int i = 0; i < ExtraSvs.Count; i++)
            {
                if (ExtraSvs[i].Id == us.Id)
                {
                    if (ExtraSvs[i].Price == ExtraSvPrice)
                    {
                        ExtraSvs[i].Name = ExtraSvName;
                        facSv.ModifyService(ExtraSvs[i]);
                        ExtraSvs.Clear();
                        facSv.GetServices().ForEach(ExtraSvs.Add);
                        ExtraSvName = null;
                        ExtraSvPrice = 0;
                        break;
                    }
                    else
                    {
                        facSv.DeleteService(ExtraSvs[i]);
                        facSv.AddExtraSvIntoDb(new ExtraServices(ExtraSvName, ExtraSvPrice));
                        ExtraSvs.Clear();
                        facSv.GetServices().ForEach(ExtraSvs.Add);
                        ExtraSvName = null;
                        ExtraSvPrice = 0;
                        break;
                    }
                }
            }
        }

        public void CancelExtraSvState(object param)
        {
            ShowExtraSvUpdateDetails = false;
            ExtraSvName = null;
            extraSvPrice = 0;
        }

     
        //Manage Deals Methods
        public void CreateDeal(object param)
        {
            if (DealName != null && NrNights > 0 && DealPrice>0 && DealEnd > DealStart)
            {
                DealServiceLayer exSv = new DealServiceLayer();         
                Deal ex = new Deal(DealName, NrNights, DealPrice, DealStart,DealEnd);
                exSv.AddDeal(ex);
                Deals.Add(ex);
                ClearData();
                MessageBox.Show("Service created");
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        public void DeleteDeal(object param)
        {
            Deal us = param as Deal;
            DealServiceLayer sv = new DealServiceLayer();
            sv.DeleteDeal(us);
            Deals.Remove(us);
        }

        public void UpdateDeal(object param)
        {
            Deal exToUp = param as Deal;
            if (exToUp != null)
            {
                DealName = exToUp.Name;
                NrNights = exToUp.NightsNr;
                DealPrice = exToUp.Price;
                DealStart = exToUp.ActiveFrom;
                DealEnd = exToUp.ActiveTo;
                ShowDealsDetails = true;
            }
        }

        public void ClearDealControls(object param)
        {
            ClearData();
        }

        public void SaveDealChanges(object param)
        {
            ShowDealsDetails = false;
            DealServiceLayer facSv = new DealServiceLayer();
            Deal us = param as Deal;
            for (int i = 0; i < Deals.Count; i++)
            {
                if (Deals[i].Id == us.Id)
                {
                    Deals[i].Name = DealName;
                    Deals[i].NightsNr = NrNights;
                    Deals[i].Price = DealPrice;
                    Deals[i].ActiveFrom = DealStart;
                    Deals[i].ActiveFrom = DealEnd;
                    facSv.ModifyDeal(Deals[i]);
                    Deals.Clear();
                    facSv.GetDeals().ForEach(Deals.Add);
                    ClearData();
                    break;
                 }           
            }
        }

        public void CancelDealChanges(object param)
        {
            ShowDealsDetails = false;
            ClearData();
        }
 
        public void ClearData()
        {
            DealName = null;
            NrNights = 0;
            DealPrice = 0;
            DealStart = DateTime.Now;
            DealEnd = DateTime.Now;
        }
    
    }
}
