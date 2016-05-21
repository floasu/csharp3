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
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private string extraSvName;
        private double extraSvPrice;
        private bool showUpdateDetails;
        private bool showExtraSvUpdateDetails;

        private ICommand createFacilityCommand;
        private ICommand deleteFacilityCommand;
        private ICommand updateFacilityCommand;
        private ICommand clearDataControlsCommand;
        private ICommand saveUpdateModificationCommand;
        private ICommand cancelUpdateStateCommand;

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
                if (facil.Count >0 )
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


        public void CreateExtraSv(object param)
        {
            if (ExtraSvName != null && ExtraSvName.Equals("") == false && ExtraSvPrice > 0 && ExtraSvPrice.Equals("") ==false)
            {
                ExtraSvServiceLayer exSv = new ExtraSvServiceLayer();
                var item = ExtraSvs.Where(p => (p.Name.Equals(ExtraSvName) && p.Price == ExtraSvPrice));
                List<ExtraServices> facil = item.ToList<ExtraServices>();
                if (facil.Count > 0 )
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
                    ExtraSvs[i].Name = ExtraSvName;
                    facSv.ModifyService(ExtraSvs[i]);
                    ExtraSvs.Clear();
                    facSv.GetServices().ForEach(ExtraSvs.Add);
                    ExtraSvName = null;
                    ExtraSvPrice = 0;
                    break;
                }
            }
        }

        public void CancelExtraSvState(object param)
        {
            ShowExtraSvUpdateDetails = false;
            ExtraSvName = null;
            extraSvPrice = 0;
        }


    }
}
