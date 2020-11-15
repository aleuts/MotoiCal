﻿using MotoiCal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MotoiCal.ViewModels.Settings
{
    abstract class SettingsContentViewModel : INotifyPropertyChanged
    {
        private ButtonManagerModel buttonManager;

        private IMotorSport motorSportSeries;

        private bool isPracticeSaved;
        private bool isQualifyingSaved;
        private bool isRaceSaved;

        public SettingsContentViewModel(IMotorSport motorSportSeries)
        {
            this.motorSportSeries = motorSportSeries;

            this.IsPracticeSaved = true;
            this.IsQualifyingSaved = true;
            this.IsRaceSaved = true;

            this.buttonManager = new ButtonManagerModel();

            this.AtEventCommand = new SynchronousRelayCommand(this.AtEvent);
            this.Minutes5EventCommand = new SynchronousRelayCommand(this.Minutes5Event);
            this.Minutes15EventCommand = new SynchronousRelayCommand(this.Minutes15Event);
            this.Minutes30EventCommand = new SynchronousRelayCommand(this.Minutes30Event);
            this.Minutes45EventCommand = new SynchronousRelayCommand(this.Minutes45Event);
            this.Minutes60EventCommand = new SynchronousRelayCommand(this.Minutes60Event);
            this.Minutes120EventCommand = new SynchronousRelayCommand(this.Minutes120Event);

            this.buttonManager.AddButton(this.AtEventButtonStatus = new ButtonStatusModel("At Event", "Set event to remind you at the time of event"));
            this.buttonManager.AddButton(this.Minutes5EventButtonStatus = new ButtonStatusModel("5 Mins", "Set event to remind you 5 Minutes before the time of event"));
            this.buttonManager.AddButton(this.Minutes15EventButtonStatus = new ButtonStatusModel("15 Mins", "Set event to remind you 15 Minutes before the time of event"));
            this.buttonManager.AddButton(this.Minutes30EventButtonStatus = new ButtonStatusModel("30 Mins", "Set event to remind you 30 Minutes before the time of event"));
            this.buttonManager.AddButton(this.Minutes45EventButtonStatus = new ButtonStatusModel("45 Mins", "Set event to remind you 45 Minutes before the time of event"));
            this.buttonManager.AddButton(this.Minutes60EventButtonStatus = new ButtonStatusModel("60 Mins", "Set event to remind you 60 Minutes before the time of event"));
            this.buttonManager.AddButton(this.Minutes120EventButtonStatus = new ButtonStatusModel("120 Mins", "Set event to remind you 120 Minutes before the time of event"));

            this.AtEventButtonStatus.ButtonStatusChanged = new EventHandler(this.AtEventButtonActive);
            this.Minutes5EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes5EventButtonActive);
            this.Minutes15EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes15EventButtonActive);
            this.Minutes30EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes30EventButtonActive);
            this.Minutes45EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes45EventButtonActive);
            this.Minutes60EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes60EventButtonActive);
            this.Minutes120EventButtonStatus.ButtonStatusChanged = new EventHandler(this.Minutes120EventButtonActive);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract bool IsQualifyingVisible { get; }
        public abstract bool IsSuperpoleVisible { get; }
        public abstract bool IsWarmupVisible { get; }
        public abstract bool IsBehindTheScenesVisible { get; }
        public abstract bool IsAfterTheFlagVisible { get; }

        public virtual bool IsPracticeSaved
        {
            get
            {
                return this.isPracticeSaved;
            }
            set
            {
                this.isPracticeSaved = value;
                this.UpdateIMotorSportEvenList(value, "Practice");
                this.OnPropertyChanged("IsPracticeSaved");
            }
        }

        public bool IsQualifyingSaved
        {
            get
            {
                return this.isQualifyingSaved;
            }
            set
            {
                this.isQualifyingSaved = value;
                this.UpdateIMotorSportEvenList(value, "Qualifying");
                this.OnPropertyChanged("IsQualifyingSaved");
            }
        }

        public bool IsRaceSaved
        {
            get
            {
                return this.isRaceSaved;
            }
            set
            {
                this.isRaceSaved = value;
                this.UpdateIMotorSportEvenList(value, "Race");
                this.OnPropertyChanged("IsRaceSaved");
            }
        }

        public bool IsEventReminderActive
        {
            get
            {
                return this.GetIMotorSportEventTriggerStatus();
            }
            set
            {
                this.SetIMotorSportEventTriggerStatus(value);
                this.OnPropertyChanged("IsEventReminderActive");
                this.OnPropertyChanged("IsEventIntervalButtonEnabled");
            }
        }
        public int EventTriggerInterval
        {
            get
            {
                return this.GetIMotorSportEventTriggerMins();
            }
            set
            {
                this.SetIMotorSportEventTriggerMins(value);
                this.OnPropertyChanged("EventTriggerInterval");
            }
        }

        public bool IsEventIntervalButtonEnabled => this.GetIMotorSportEventTriggerStatus();

        public SynchronousRelayCommand AtEventCommand { get; }
        public SynchronousRelayCommand Minutes5EventCommand { get; }
        public SynchronousRelayCommand Minutes15EventCommand { get; }
        public SynchronousRelayCommand Minutes30EventCommand { get; }
        public SynchronousRelayCommand Minutes45EventCommand { get; }
        public SynchronousRelayCommand Minutes60EventCommand { get; }
        public SynchronousRelayCommand Minutes120EventCommand { get; }

        public ButtonStatusModel AtEventButtonStatus { get; set; }
        public ButtonStatusModel Minutes5EventButtonStatus { get; set; }
        public ButtonStatusModel Minutes15EventButtonStatus { get; set; }
        public ButtonStatusModel Minutes30EventButtonStatus { get; set; }
        public ButtonStatusModel Minutes45EventButtonStatus { get; set; }
        public ButtonStatusModel Minutes60EventButtonStatus { get; set; }
        public ButtonStatusModel Minutes120EventButtonStatus { get; set; }

        private void AtEventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("AtEventButtonStatus");
        }

        private void Minutes5EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes5EventButtonStatus");
        }

        private void Minutes15EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes15EventButtonStatus");
        }

        private void Minutes30EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes30EventButtonStatus");
        }

        private void Minutes45EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes45EventButtonStatus");
        }

        private void Minutes60EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes60EventButtonStatus");
        }

        private void Minutes120EventButtonActive(object sender, EventArgs e)
        {
            this.OnPropertyChanged("Minutes120EventButtonStatus");
        }

        private void AtEvent()
        {
            this.buttonManager.SetActiveButton(this.AtEventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.AtTimeOfEvent;
        }

        private void Minutes5Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes5EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes5;
        }

        private void Minutes15Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes15EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes15;
        }

        private void Minutes30Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes30EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes30;
        }

        private void Minutes45Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes45EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes45;
        }

        private void Minutes60Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes60EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes60;
        }

        private void Minutes120Event()
        {
            this.buttonManager.SetActiveButton(this.Minutes120EventButtonStatus);
            this.EventTriggerInterval = (int)CalendarEventTrigger.Minutes120;
        }

        private bool GetIMotorSportEventTriggerStatus()
        {
            return this.motorSportSeries.IsEventReminderActive;
        }

        private void SetIMotorSportEventTriggerStatus(bool isEventTriggerActive)
        {
            this.motorSportSeries.IsEventReminderActive = isEventTriggerActive;
        }

        private int GetIMotorSportEventTriggerMins()
        {
            return this.motorSportSeries.EventReminderMins;
        }

        private void SetIMotorSportEventTriggerMins(int minsToTrigger)
        {
            this.motorSportSeries.EventReminderMins = minsToTrigger;
        }

        // Sets if the event is scraped or not depending on bool value.
        public void UpdateIMotorSportEvenList(bool isEventEnabled, string eventName)
        {
            if (!isEventEnabled)
            {
                this.motorSportSeries.ExcludedEvents.Add(eventName);
            }
            else
            {
                this.motorSportSeries.ExcludedEvents.Remove(eventName);
            }
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
