using Festival.Model;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festival__App_Second.viewmodel
{
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; }
        }

        private ObservableCollection<LineUp> _lineUp;

        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                if (_lineUp == null)
                {
                    _lineUp = LineUp.GetLineUp();
                }

                return _lineUp;

            }
            set { _lineUp = value; OnPropertyChanged("LineUps"); }
        }

        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get
            {
                if (_stages == null)
                {
                    _stages = Stage.GetStages();
                }

                return _stages;

            }
            set { _stages = value; OnPropertyChanged("Stages"); }
        }

        private ObservableCollection<DateTime> _days;

        public ObservableCollection<DateTime> Days
        {
            get
            {
                if (_days == null)
                {
                    _days = Festival.Model.Festival.GetUniqueDays();
                }

                return _days;

            }
            set { _days = value; OnPropertyChanged("Days"); }
        }

        private ObservableCollection<Band> _bands;

        public ObservableCollection<Band> Bands
        {
            get
            {
                if (_bands == null)
                {
                    _bands = Band.GetBands();
                }
                return _bands;
            }
            set { _bands = value; OnPropertyChanged("Bands"); }
        }

        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get 
            {
                if (_genres == null)
                {
                    _genres = Genre.GetGenres();
                }
                return _genres; 
            }
            set { _genres = value; OnPropertyChanged("Genres"); }
        }

        private Genre _selectedGenre1;

        public Genre SelectedGenre1
        {
            get { return _selectedGenre1; }
            set 
            { 
                _selectedGenre1 = value;
                OnPropertyChanged("SelectedGenre1");
            }
        }

        private Genre _selectedGenre2;

        public Genre SelectedGenre2
        {
            get { return _selectedGenre2; }
            set
            {
                _selectedGenre2 = value;
                OnPropertyChanged("SelectedGenre2");
            }
        }
        

        private Band _selectedBand;

        public Band SelectedBand
        {
            get { return _selectedBand; }
            set 
            { 
                _selectedBand = value;
                NewLineUp.Band = SelectedBand;
                OnPropertyChanged("SelectedBand"); }
        }

        private DateTime _selectedDay;

        public DateTime SelectedDay
        {
            get { return _selectedDay; }
            set 
            { 
                _selectedDay = value;
                NewLineUp.Date = SelectedDay;
                OnPropertyChanged("SelectedDay");
            }
        }
        

        private string _selectedFrom;

        public string SelectedFrom
        {
            get { return _selectedFrom; }
            set 
            { 
                _selectedFrom = value;
                NewLineUp.From = SelectedFrom;
                OnPropertyChanged("SelectedFrom"); }
        }

        private string _selectedUntil;

        public string SelectedUntil
        {
            get { return _selectedUntil; }
            set 
            { 
                _selectedUntil = value;
                NewLineUp.Until = SelectedUntil;
                OnPropertyChanged("SelectedUntil");
            
            }
        }

        private Stage _selectedStage;

        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set 
            { 
                _selectedStage = value;
                NewLineUp.Stage = SelectedStage;
                SelectedStageList = LineUp.GetLineUpByStage(SelectedStage);
                StageName = SelectedStage.Name;
                OnPropertyChanged("SelectedStage");       
            }
        }

        private LineUp _newLineUp;

        public LineUp NewLineUp
        {
            get 
            {
                if (_newLineUp == null)
                {
                    _newLineUp = new LineUp();
                }
                
                return _newLineUp; 
            }
            set
            {
                _newLineUp = value;
                ; OnPropertyChanged("NewLineUp"); }
        }

        private Band _newBand;

        public Band NewBand
        {
            get 
            {
                if (_newBand == null)
                {
                    _newBand = new Band();
                }
                return _newBand; 
            }
            set 
            {
                _newBand = value;
                OnPropertyChanged("NewBand");
            }
        }

        private Genre _newGenre;

        public Genre NewGenre
        {
            get 
            {
                if (_newGenre == null)
                {
                    _newGenre = new Genre();
                }
                
                return _newGenre; }
            set { _newGenre = value; OnPropertyChanged("NewGenre"); }
        }

        private Stage _newStage;

        public Stage NewStage
        {
            get 
            {
                if (_newStage == null)
                {
                    _newStage = new Stage();
                }
                return _newStage; }
            set { _newStage = value; OnPropertyChanged("NewStage"); }
        }
        
        

        private ObservableCollection<LineUp> _selectedStageList;

        public ObservableCollection<LineUp> SelectedStageList
        {
            get 
            {
                if (_selectedStageList == null)
                {
                    _selectedStageList = LineUp.GetLineUp();
                }

                return _selectedStageList; 
            }
            set { _selectedStageList = value; OnPropertyChanged("SelectedStageList"); }
        }

        private string _stageName;

        public string StageName
        {
            get 
            {
                if (_stageName == null)
                {
                    _stageName = "All Stages";
                }
                
                return _stageName; 
            }
            set { _stageName = value; OnPropertyChanged("StageName"); }
        }
        

        public ICommand AddCommand
        {
            get { return new RelayCommand(AddLineUp); }
        }

        private void AddLineUp()
        {
            NewLineUp.Band = SelectedBand;
            NewLineUp.Stage = SelectedStage;
            NewLineUp.Date = SelectedDay;
            NewLineUp.From = SelectedFrom;
            NewLineUp.Until = SelectedUntil;

            LineUp.AddLineUp(NewLineUp);
            SelectedStageList = LineUp.GetLineUpByStage(SelectedStage);
        }

        public ICommand OpenFileCommand
        {
            get { return new RelayCommand(AddPicture); }
        }

        private void AddPicture()
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    string fn = ofd.FileName;
                    string extension = Path.GetExtension(fn);

                    System.IO.File.Copy(fn, AppDomain.CurrentDomain.BaseDirectory + NewBand.Name + extension);

                    NewBand.Picture=AppDomain.CurrentDomain.BaseDirectory + NewBand.Name + extension;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            
        }

        public ICommand AddBandCommand
        {
            get { return new RelayCommand(AddBand); }
        }

        public ICommand OpenAddStageCommand
        {
            get { return new RelayCommand(OpenStage); }
        }

        public ICommand SaveStageCommand
        {
            get { return new RelayCommand(SaveStage); }
        }

        public ICommand OpenAddGenreCommand
        {
            get { return new RelayCommand(OpenGenre); }
        }

        public ICommand SaveGenreCommand
        {
            get { return new RelayCommand(SaveGenre); }
        }

        private void SaveGenre()
        {
            Genre.AddGenre(NewGenre);
            Genres = Genre.GetGenres();
        }

        private void OpenGenre()
        {
            Festival__App_Second.view.AddGenre w = new Festival__App_Second.view.AddGenre();
            w.Show();
        }

        private void SaveStage()
        {
            Stage.AddStage(NewStage);
            Stages = Stage.GetStages();
        }

        private void OpenStage()
        {
            Festival__App_Second.view.AddStage w = new Festival__App_Second.view.AddStage();
            w.Show();
        }

        private void AddBand()
        {
            ObservableCollection<Genre> ocGenre = new ObservableCollection<Genre>();
            ocGenre.Add(SelectedGenre1);
            ocGenre.Add(SelectedGenre2);

            NewBand.Genres = ocGenre;
            AddPictureFtp();

            Band.AddBand(NewBand);
            Bands = Band.GetBands();
        }

        private void AddPictureFtp()
        {
            string filename = NewBand.Picture;
            String extension = Path.GetExtension(NewBand.Picture);
            string ftpServerIP = "ftp.taalvakantie.be/httpdocs/festival/";
            string ftpUserName = "taalvakantie";
            string ftpPassword = "KG54sbljBB";

            FileInfo objFile = new FileInfo(filename);
            FtpWebRequest objFTPRequest;

            // Create FtpWebRequest object 
            objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + objFile.Name));

            // Set Credintials
            objFTPRequest.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            
            // By default KeepAlive is true, where the control connection is 
            // not closed after a command is executed.
            objFTPRequest.KeepAlive = false;

            // Set the data transfer type.
            objFTPRequest.UseBinary = true;

            // Set content length
            objFTPRequest.ContentLength = objFile.Length;

            // Set request method
            objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

            // Set buffer size
            int intBufferLength = 16 * 1024;
            byte[] objBuffer = new byte[intBufferLength];

            // Opens a file to read
            FileStream objFileStream = objFile.OpenRead();

            try
                {
                     // Get Stream of the file
                     Stream objStream = objFTPRequest.GetRequestStream();

                        int len = 0;

                        while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
                        {
                            // Write file Content 
                            objStream.Write(objBuffer, 0, len);

                        }

                        objStream.Close();
                        objFileStream.Close();
                        NewBand.Picture = "http://www.taalvakantie.be/festival/" + NewBand.Name + extension;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
        }
    }
}