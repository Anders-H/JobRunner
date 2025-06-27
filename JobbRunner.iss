#define MyAppName "JobRunner"
#define MyAppVersion "1.8 (preview)"
#define MyAppPublisher "Anders Hesselbom"
#define MyAppURL "https://github.com/Anders-H/JobRunner"
#define MyAppExeName "JobRunner.exe"

[Setup]
AppId={{8DC4112D-E21B-44DB-90D2-375BEBEE04DE}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequiredOverridesAllowed=dialog
OutputDir=D:\GitRepos\JobRunner
OutputBaseFilename=JobRunnerSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\GitRepos\JobRunner\JobRunner\bin\Release\JobRunner.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\GitRepos\JobRunner\JobRunner\bin\Release\JobRunner.exe.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
