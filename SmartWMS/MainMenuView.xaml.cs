using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWMS.Models;
using SmartWMS.OperationViews;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartWMS
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuView : MasterDetailPage
    {
        #region

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private SmartWMSPage selectedPage;
        public SmartWMSPage SelectedPage
        {
            get => selectedPage;
            set
            {
                selectedPage = value;
                OnPropertyChanged(nameof(SelectedPage));
            }
        }

        private List<SmartWMSPage> pages;
        public List<SmartWMSPage> Pages
        {
            get => pages;
            set
            {
                pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        #endregion

        private ISpeechToText _speechRecongnitionInstance;
        private bool isMicrophoneOpen = false;
        public MainMenuView()
        {
            Pages = new List<SmartWMSPage>();

            Pages.Add(new SmartWMSPage("Anasayfa", typeof(MainPageView)));
            Pages.Add(new SmartWMSPage("Tablolar", typeof(TablesView)));
            Pages.Add(new SmartWMSPage("Ürün Alım", typeof(GoodsReceiptView)));
            Pages.Add(new SmartWMSPage("Depolama Görevleri", typeof(StorageTaskView)));
            Pages.Add(new SmartWMSPage("Toplama", typeof(PickingView)));
            Pages.Add(new SmartWMSPage("Yük Arabası Turu", typeof(TrolleyTourView)));
            Pages.Add(new SmartWMSPage("Yükleme", typeof(LoadingView)));
            Pages.Add(new SmartWMSPage("Sevk", typeof(DispatchingView)));
            Pages.Add(new SmartWMSPage("Stok İşlemleri", typeof(StockOperationsView)));
            Pages.Add(new SmartWMSPage("Denetim Sayım", typeof(CycleCountView)));

            InitializeComponent();

            
            try
            {
                _speechRecongnitionInstance = DependencyService.Get<ISpeechToText>();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }
            
            //MessagingCenter.Subscribe<ISpeechToText, string>(this, "STT", (sender, args) =>
            //{
            //    if (App.speechView == this.GetType().Name)
            //    {
            //        SpeechToTextFinalResultRecieved(args);

            //    }
            //});

            //MessagingCenter.Subscribe<ISpeechToText>(this, "Final", (sender) =>
            //{
            //    //start.IsEnabled = true;
            //});
            
            MessagingCenter.Subscribe<IMessageSender, string>(this, "STT", (sender, args) =>
            {
                if (App.speechView == this.GetType().Name)
                {
                    isMicrophoneOpen = false;
                    SpeechToTextFinalResultRecieved(args);
                }
                
            });
            
            BindingContext = this;

            Detail = new NavigationPage(new MainPageView());
            IsPresented = false;

        }
        /*
        protected override void OnDisappearing()
        {
            if (!isMicrophoneOpen)
            {
                MessagingCenter.Unsubscribe<IMessageSender, string>(this, "STT");
            }
            base.OnDisappearing();

        }*/

        /*
        private void OpenPage(string name)
        {
            if(Pages.Where(p => p.ViewName.Contains(name)).Any())
            {
                var page = pages.Where(p => p.ViewName.Contains(name)).FirstOrDefault();
            }
            
        }*/

        private void SmartWMSPageList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (SelectedPage.ViewName.Equals("Anasayfa"))
                Detail = new NavigationPage(new MainPageView());
            else if (SelectedPage.ViewName.Equals("Tablolar"))
                Detail = new NavigationPage(new TablesView());
            else if (SelectedPage.ViewName.Equals("Ürün Alım"))
                Detail = new NavigationPage(new GoodsReceiptView());
            else if(SelectedPage.ViewName.Equals("Depolama Görevleri"))
                Detail = new NavigationPage(new StorageTaskView());
            else if (SelectedPage.ViewName.Equals("Toplama"))
                Detail = new NavigationPage(new PickingView());
            else if (SelectedPage.ViewName.Equals("Yük Arabası Turu"))
                Detail = new NavigationPage(new TrolleyTourView());
            else if (SelectedPage.ViewName.Equals("Yükleme"))
                Detail = new NavigationPage(new LoadingView());
            else if (SelectedPage.ViewName.Equals("Sevk"))
                Detail = new NavigationPage(new DispatchingView());
            else if (SelectedPage.ViewName.Equals("Stok İşlemleri"))
                Detail = new NavigationPage(new StockOperationsView());
            else if (SelectedPage.ViewName.Equals("Denetim Sayım"))
                Detail = new NavigationPage(new CycleCountView());

            IsPresented = false;
        }

        private void SpeechToTextFinalResultRecieved(string args)
        {
            //recon.Text = args;

            int minDistance = 99999;
            int index = 0;


            // LevenshteinDistance is a useful static class that allows us to find similarity between strings
            // In this loop we try to find appropriate string in master page with user voice input
            for (int i = 0; i < Pages.Count; i++)
            {
                if (LevenshteinDistance.Compute(args, Pages[i].ViewName) < minDistance)
                {
                    minDistance = LevenshteinDistance.Compute(args, Pages[i].ViewName);
                    index = i;
                }
            }
            switch (index)
            {
                case 0:
                    Detail = new NavigationPage(new MainPageView());
                    break;
                case 1:
                    Detail = new NavigationPage(new TablesView());
                    break;
                case 2:
                    Detail = new NavigationPage(new GoodsReceiptView());
                    break;
                case 3:
                    Detail = new NavigationPage(new StorageTaskView());
                    break;
                case 4:
                    Detail = new NavigationPage(new PickingView());
                    break;
                case 5:
                    Detail = new NavigationPage(new TrolleyTourView());
                    break;
                case 6:
                    Detail = new NavigationPage(new LoadingView());
                    break;
                case 7:
                    Detail = new NavigationPage(new DispatchingView());
                    break;
                case 8:
                    Detail = new NavigationPage(new StockOperationsView());
                    break;
                case 9:
                    Detail = new NavigationPage(new CycleCountView());
                    break;

            }

            IsPresented = false;
        }

        private void start_Clicked(object sender, EventArgs e)
        {

            //StartRecording();
        }
        
        private void ButtonMicrophone_Clicked(object sender, EventArgs e)
        {
            
            isMicrophoneOpen = true;
            App.speechView = this.GetType().Name;

            StartRecording();
        }
        
        private void StartRecording()
        {
            try
            {
                _speechRecongnitionInstance.StartSpeechToText();
            }
            catch (Exception ex)
            {
                //recon.Text = ex.Message;
            }
        }

    }

}