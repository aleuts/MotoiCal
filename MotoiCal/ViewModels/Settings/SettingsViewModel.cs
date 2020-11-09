﻿using MotoiCal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MotoiCal.ViewModels.Settings;
using MotoiCal.Views;
using MotoiCal.Views.Settings;

namespace MotoiCal.ViewModels.Settings
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        private ButtonManagerModel buttonManagerModel;

        private FrameworkElement settingsContentView;

        private Formula1SettingsContentViewModel formula1SettingsContent;
        private MotoGPSettingsContentViewModel motoGPSettingsContent;
        private WorldSBKSettingsContentViewModel worldSBKSettingsContent;

        private IMotorSport formula1;
        private IMotorSport motoGP;
        private IMotorSport worldSBK;

        public SettingsViewModel(IMotorSport formula1, IMotorSport motoGP, IMotorSport worldSBK)
        {
            this.formula1 = formula1;
            this.motoGP = motoGP;
            this.worldSBK = worldSBK;

            this.buttonManagerModel = new ButtonManagerModel();

            this.FormulaOneParametersCommand = new SynchronousRelayCommand(this.FormulaOneParameters);
            this.MotoGPParametersCommand = new SynchronousRelayCommand(this.MotoGPParameters);
            this.WorldSBKParametersCommand = new SynchronousRelayCommand(this.WorldSBKParameters);

            this.buttonManagerModel.AddButton(this.FormulaOneParametersButtonStatus = new ButtonStatusModel("Formula One", "Configure Formula One Search Settings"));
            this.buttonManagerModel.AddButton(this.MotoGPParametersButtonStatus = new ButtonStatusModel("MotoGP", "Configure MotoGP Search Settings"));
            this.buttonManagerModel.AddButton(this.WorldSBKParametersButtonStatus = new ButtonStatusModel("World SBK", "Configure World SBK Search Settings"));

            this.FormulaOneParametersButtonStatus.ButtonStatusChanged = new EventHandler(this.FormulaOneButtonActive);
            this.MotoGPParametersButtonStatus.ButtonStatusChanged = new EventHandler(this.MotoGPButtonActive);
            this.WorldSBKParametersButtonStatus.ButtonStatusChanged = new EventHandler(this.WorldSBKButtonActive);

            this.formula1SettingsContent = new Formula1SettingsContentViewModel(this.formula1);
            this.motoGPSettingsContent = new MotoGPSettingsContentViewModel(this.motoGP);
            this.worldSBKSettingsContent = new WorldSBKSettingsContentViewModel(this.worldSBK);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SynchronousRelayCommand FormulaOneParametersCommand { get; }
        public SynchronousRelayCommand MotoGPParametersCommand { get; }
        public SynchronousRelayCommand WorldSBKParametersCommand { get; }

        public ButtonStatusModel FormulaOneParametersButtonStatus { get; set; }
        public ButtonStatusModel MotoGPParametersButtonStatus { get; set; }
        public ButtonStatusModel WorldSBKParametersButtonStatus { get; set; }

        public FrameworkElement SettingsContentView
        {
            get { return this.settingsContentView; }
            set
            {
                this.settingsContentView = value;
                this.OnPropertyChanged("SettingsContentView");
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void FormulaOneButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("FormulaOneParametersButtonStatus");
        }

        private void MotoGPButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("MotoGPParametersButtonStatus");
        }

        private void WorldSBKButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("WorldSBKParametersButtonStatus");
        }

        private void FormulaOneParameters()
        {
            this.buttonManagerModel.SetActiveButton(this.FormulaOneParametersButtonStatus);
            this.SettingsContentView = new SettingsContentView();
            this.SettingsContentView.DataContext = this.formula1SettingsContent;
        }

        private void MotoGPParameters()
        {
            this.buttonManagerModel.SetActiveButton(this.MotoGPParametersButtonStatus);
            this.SettingsContentView = new SettingsContentView();
            this.SettingsContentView.DataContext = this.motoGPSettingsContent;
        }

        private void WorldSBKParameters()
        {
            this.buttonManagerModel.SetActiveButton(this.WorldSBKParametersButtonStatus);
            this.SettingsContentView = new SettingsContentView();
            this.SettingsContentView.DataContext = this.worldSBKSettingsContent;
        }
    }
}
