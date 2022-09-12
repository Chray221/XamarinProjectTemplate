using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamProjectTemplate
{
    public class FocusButton : Button
    {
        public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create(
                propertyName: nameof(UnfocusedCommand),
                returnType: typeof(ICommand),
                declaringType: typeof(FocusButton),
                defaultValue: null);
        public ICommand UnfocusedCommand
        {
            get => (ICommand)GetValue(UnfocusedCommandProperty);
            set => SetValue(UnfocusedCommandProperty, value);
        }

        public FocusButton()
        {
            Unfocused += FocusButton_Unfocused;
        }

        private void FocusButton_Unfocused(object sender, FocusEventArgs e)
        {
            ExecuteUnfocusCommand();
        }

        public void ExecuteUnfocusCommand()
        {
            if (UnfocusedCommand != null && UnfocusedCommand.CanExecute(null))
                UnfocusedCommand?.Execute(null);
        }
    }
}
