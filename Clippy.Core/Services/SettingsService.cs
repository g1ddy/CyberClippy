using System;
using System.IO;
using System.Text.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Clippy.Core.Services
{
    public class SettingsService : ISettingsService, INotifyPropertyChanged
    {
        private readonly string _settingsFilePath;
        private SettingsData _settings;

        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsService(string settingsFilePath = null)
        {
            if (string.IsNullOrEmpty(settingsFilePath))
            {
                var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Clippy");
                Directory.CreateDirectory(appDataPath);
                _settingsFilePath = Path.Combine(appDataPath, "settings.json");
            }
            else
            {
                _settingsFilePath = settingsFilePath;
                var directory = Path.GetDirectoryName(_settingsFilePath);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }

            LoadSettings();
        }

        private void LoadSettings()
        {
            if (File.Exists(_settingsFilePath))
            {
                try
                {
                    var json = File.ReadAllText(_settingsFilePath);
                    _settings = JsonSerializer.Deserialize<SettingsData>(json) ?? new SettingsData();
                }
                catch
                {
                    _settings = new SettingsData();
                }
            }
            else
            {
                _settings = new SettingsData();
            }
        }

        private void SaveSettings()
        {
            var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions { WriteIndented = true });

            Task.Run(async () =>
            {
                try
                {
#if NETSTANDARD2_1
                    // File.WriteAllTextAsync isn't guaranteed in all NS2.0/2.1 contexts depending on the underlying BCL, but we can write to it via FileStream or rely on the fact we are off the UI thread via Task.Run.
                    File.WriteAllText(_settingsFilePath, json);
#else
                    await File.WriteAllTextAsync(_settingsFilePath, json);
#endif
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving settings: {ex.Message}");
                }
            });
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool AutoPin
        {
            get => _settings.AutoPin;
            set
            {
                if (_settings.AutoPin != value)
                {
                    _settings.AutoPin = value;
                    SaveSettings();
                    OnPropertyChanged();
                }
            }
        }

        public bool TrayClippy
        {
            get => _settings.TrayClippy;
            set
            {
                if (_settings.TrayClippy != value)
                {
                    _settings.TrayClippy = value;
                    SaveSettings();
                    OnPropertyChanged();
                }
            }
        }

        public bool TranslucentBackground
        {
            get => _settings.TranslucentBackground;
            set
            {
                if (_settings.TranslucentBackground != value)
                {
                    _settings.TranslucentBackground = value;
                    SaveSettings();
                    OnPropertyChanged();
                }
            }
        }

        public bool KeyboardEnabled
        {
            get => _settings.KeyboardEnabled;
            set
            {
                if (_settings.KeyboardEnabled != value)
                {
                    _settings.KeyboardEnabled = value;
                    SaveSettings();
                    OnPropertyChanged();
                }
            }
        }

        public int Tokens
        {
            get => _settings.Tokens;
            set
            {
                var clampedValue = Math.Max(50, Math.Min(2000, value));
                if (_settings.Tokens != clampedValue)
                {
                    _settings.Tokens = clampedValue;
                    SaveSettings();
                    OnPropertyChanged();
                }
            }
        }

        private class SettingsData
        {
            public bool AutoPin { get; set; } = true;
            public bool TrayClippy { get; set; } = true;
            public bool TranslucentBackground { get; set; } = true;
            public bool KeyboardEnabled { get; set; } = true;
            public int Tokens { get; set; } = 100;
        }
    }
}
