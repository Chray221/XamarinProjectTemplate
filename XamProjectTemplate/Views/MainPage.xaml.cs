using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamProjectTemplate
{
    public partial class MainPage
    {
        private Command _unfocusedCommand;
        public Command UnfocusedCommand
        {
            get => _unfocusedCommand ?? (_unfocusedCommand = new Command(ExecuteUnfocusedCommand));
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void Button_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            App.Log("Focused");
        }

        void Button_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(IsFocused))
            {
                App.Log("==>> Focused");
            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if(sender is Button button)
            {
                bool buttonIsFocused = button.Focus();
            }
        }
        private static void ExecuteUnfocusedCommand(object obj)
        {
            App.Log("++++++>>>>>> UNFocused");
        }
    }
}
