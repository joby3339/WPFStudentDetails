# WPF Student Details Application

A Windows Presentation Foundation (WPF) application for managing student details.

## 🚀 Features

- Student data management
- MVVM architecture
- JSON data persistence
- Modern WPF UI

## 🔧 Development

### Prerequisites
- .NET 8.0 or later
- Visual Studio 2022 or VS Code
- Windows 10/11

### Building the Application
```bash
dotnet restore
dotnet build --configuration Release
```

### Running the Application
```bash
dotnet run --project WPFStudentDetails/WPFStudentDetails.csproj
```

## 🚀 CI/CD Pipeline

This project uses GitHub Actions for continuous integration and deployment:

### Workflows

1. **CI/CD Pipeline** (`.github/workflows/ci-cd.yml`)
   - Triggers on push to `main` and `develop` branches
   - Builds and tests the application
   - Creates artifacts for each build
   - Automatically creates release packages on GitHub releases

2. **Dependency Updates** (`.github/workflows/dependency-update.yml`)
   - Runs weekly to check for package updates
   - Automatically creates pull requests with dependency updates

### Pipeline Features

- ✅ Automated building and testing
- ✅ Multi-platform support (win-x64, win-x86)
- ✅ Artifact generation
- ✅ Automated releases
- ✅ Code quality checks
- ✅ Security scanning
- ✅ Dependency management

### Creating a Release

To create a new release:

1. Go to the GitHub repository
2. Click on "Releases" → "Create a new release"
3. Create a new tag (e.g., `v1.0.0`)
4. Write release notes
5. Click "Publish release"

The CI/CD pipeline will automatically build and attach the application binaries to the release.

## 📁 Project Structure

```
WPFStudentDetails/
├── .github/workflows/          # GitHub Actions workflows
├── WPFStudentDetails/
│   ├── Models/                 # Data models
│   ├── ViewModels/            # MVVM view models
│   ├── MainWindow.xaml        # Main UI
│   ├── App.xaml               # Application configuration
│   └── Students.json          # Data file
└── README.md
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

The CI/CD pipeline will automatically test your changes!

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.