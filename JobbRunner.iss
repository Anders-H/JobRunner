#define MyAppName "JobRunner"
#define MyAppVersion "1.4"
#define MyAppPublisher "Anders Hesselbom"
#define MyAppURL "http://winsoft.se/"
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
OutputDir=C:\GitRepos\JobRunner
OutputBaseFilename=JobRunner
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\GitRepos\JobRunner\JobRunner\bin\Release\JobRunner.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\GitRepos\JobRunner\JobRunner\bin\Release\JobRunner.exe.config"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
