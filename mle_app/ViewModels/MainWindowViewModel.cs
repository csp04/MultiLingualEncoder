using mle_app.Common;
using mle_app.Common.AsyncObject;
using mle_app.Models;
using multilingualencoderlib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace mle_app.ViewModels
{
    public class MainWindowViewModel : NotifyBase
    {
        private readonly ProducerConsumer<Action> pc;

        public EncoderModel SelectedItem
        {
            get
            {
                return Get<EncoderModel>();
            }
            set
            {
                Set(value);
            }
        }

        public string InputText
        {
            get
            {
                return Get<string>();
            }
            set
            {
                Set(value);
            }
        }


        public ObservableCollection<EncoderModel> EncoderList
        {
            get
            {
                return Get<AsyncObservableCollection<EncoderModel>>();
            }
            set
            {
                Set(value);
            }
        }

        public MainWindowViewModel()
        {
            pc = new ProducerConsumer<Action>() { AlwaysConsumeLastItem = true };

            pc.Next += (s, method) => method.Invoke();

            EncoderList = new AsyncObservableCollection<EncoderModel>();
            this.PropertyChanged += (_, e) =>
            {

                if (e.PropertyName == "InputText")
                {
                    pc.Produce(() =>
                   {
                       var encoders = Encod3r.GetEncodings().Select(enc => new EncoderModel(enc) { Text = InputText })
                             .Where(encm => encm.IsValid);

                       EncoderList = new AsyncObservableCollection<EncoderModel>(encoders);
                   });


                }
            };

            InputText = "똠양꿍";
        }


        public static DependencyProperty RegisterCommandBindingsProperty = DependencyProperty.RegisterAttached("RegisterCommandBindings", typeof(CommandBindingCollection), typeof(MainWindowViewModel), new PropertyMetadata(null, OnRegisterCommandBindingChanged));

        public static void SetRegisterCommandBindings(UIElement element, CommandBindingCollection value)
        {
            if (element != null)
                element.SetValue(RegisterCommandBindingsProperty, value);
        }
        public static CommandBindingCollection GetRegisterCommandBindings(UIElement element)
        {
            return (element != null ? (CommandBindingCollection)element.GetValue(RegisterCommandBindingsProperty) : null);
        }
        private static void OnRegisterCommandBindingChanged
        (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element != null)
            {
                CommandBindingCollection bindings = e.NewValue as CommandBindingCollection;
                if (bindings != null)
                {
                    element.CommandBindings.AddRange(bindings);
                }
            }
        }


    }
}
