[Setup] 
;Variables go here 
AppName=Energeniaal 
AppVerName=Energeniaal version 3.1
DefaultDirName={pf}\Energeniaal 
DefaultGroupName=Energeniaal 
OutputBaseFilename=Energeniaal-v3.1-Setup
LanguageDetectionMethod=uilanguage
SetupIconFile=..\Setup\energeniaal.ico
SourceDir=..\Energeniaal
OutputDir=..\Setup\Output

[Files]
Source: bin\Release\Energeniaal.exe.config; DestDir: {app}; Flags: comparetimestamp; 
Source: bin\Release\System.Data.SQLite.dll; DestDir: {app}; Flags: comparetimestamp; 
Source: bin\Release\Energeniaal.exe; DestDir: {app}; Flags: comparetimestamp; 
Source: ..\Setup\energeniaal.ico; DestDir: {app}; Flags: comparetimestamp; 
Source: bin\Release\Energeniaal.pdb; DestDir: {app}; Flags: comparetimestamp; 
Source: VariableLogos\EnergeniaalTextLogo.png; DestDir: {app}; DestName: VarLogo.png; 
Source: bin\Release\de\Energeniaal.resources.dll; DestDir: {app}\de\; Flags: comparetimestamp; 
Source: bin\Release\es\Energeniaal.resources.dll; DestDir: {app}\es\; Flags: comparetimestamp; 
Source: bin\Release\it\Energeniaal.resources.dll; DestDir: {app}\it\; Flags: comparetimestamp; 
Source: bin\Release\nl\Energeniaal.resources.dll; DestDir: {app}\nl\; Flags: comparetimestamp; 

[Dirs]
Name: {app}\es; 
Name: {app}\it; 
Name: {app}\nl;
Name: {app}\de; 

[Tasks] 
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}" 
      
[Icons]
Name: "{group}\Energeniaal"; Filename: "{app}\Energeniaal.exe"; IconFilename: "{app}\energeniaal.ico"
Name: "{group}\{cm:UninstallProgram,Energeniaal}"; Filename: "{uninstallexe}"
Name: "{userdesktop}\Energeniaal"; Filename: "{app}\Energeniaal.exe"; IconFilename: "{app}\energeniaal.ico"; Tasks: desktopicon

[Run] 
Filename: "{app}\Energeniaal.exe"; Description: "{cm:LaunchProgram,Energeniaal}"; Flags: nowait postinstall skipifsilent 

[Languages]
Name: "English"; MessagesFile: "compiler:Default.isl"
Name: "Dutch"; MessagesFile: "compiler:Languages\Dutch.isl"
Name: "Italian"; MessagesFile: "compiler:Languages\Italian.isl"
Name: "Spanish"; MessagesFile: "compiler:Languages\Spanish.isl"
Name: "German"; MessagesFile: "compiler:Languages\German.isl"

[CustomMessages]
English.DotNetVersionErrorMessage=Energeniaal requires Microsoft .NET Framework 4.0 Client Profile.%n%nPlease use Windows Update to install this version%nand then re-run the Energeniaal setup program.
Dutch.DotNetVersionErrorMessage=Energeniaal heeft Microsoft .NET Framework 4.0 Client Profile nodig.%n%nGebruik a.u.b. Windows Update om deze versie te installeren%nen dan Energeniaal setup opnieuw draaien.
Spanish.DotNetVersionErrorMessage=Energeniaal necesita Microsoft .NET Framework 4.0 Client Profile.%n%nPor favor actualice su version en Windows%ny reanude de nuevo el programa Energeniaal.
Italian.DotNetVersionErrorMessage=Energeniaal richiede Microsoft .NET Framework 4.0 Client Profile.%n%nUtilizzare Windows Update per installare questa versione%ne quindi eseguire nuovamente il programma di installazione Energeniaal.
German.DotNetVersionErrorMessage=Energeniaal benötigt Microsoft .NET Framework 4.0 Client Profile.%n%nBitte benutze Windows Update um diese zu installieren%nund fürhe anschliessend das Setup erneut aus.

[Code]
function IsDotNetDetected(version: string; service: cardinal): boolean;
// Indicates whether the specified version and service pack of the .NET Framework is installed.
//
// version -- Specify one of these strings for the required .NET Framework version:
//    'v1.1.4322'     .NET Framework 1.1
//    'v2.0.50727'    .NET Framework 2.0
//    'v3.0'          .NET Framework 3.0
//    'v3.5'          .NET Framework 3.5
//    'v4\Client'     .NET Framework 4.0 Client Profile
//    'v4\Full'       .NET Framework 4.0 Full Installation
//    'v4.5'          .NET Framework 4.5
//
// service -- Specify any non-negative integer for the required service pack level:
//    0               No service packs required
//    1, 2, etc.      Service pack 1, 2, etc. required
var
    key: string;
    install, release, serviceCount: cardinal;
    check45, success: boolean;
begin
    // .NET 4.5 installs as update to .NET 4.0 Full
    if version = 'v4.5' then begin
        version := 'v4\Full';
        check45 := true;
    end else
        check45 := false;

    // installation key group for all .NET versions
    key := 'SOFTWARE\Microsoft\NET Framework Setup\NDP\' + version;

    // .NET 3.0 uses value InstallSuccess in subkey Setup
    if Pos('v3.0', version) = 1 then begin
        success := RegQueryDWordValue(HKLM, key + '\Setup', 'InstallSuccess', install);
    end else begin
        success := RegQueryDWordValue(HKLM, key, 'Install', install);
    end;

    // .NET 4.0/4.5 uses value Servicing instead of SP
    if Pos('v4', version) = 1 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Servicing', serviceCount);
    end else begin
        success := success and RegQueryDWordValue(HKLM, key, 'SP', serviceCount);
    end;

    // .NET 4.5 uses additional value Release
    if check45 then begin
        success := success and RegQueryDWordValue(HKLM, key, 'Release', release);
        success := success and (release >= 378389);
    end;

    result := success and (install = 1) and (serviceCount >= service);
end;

function InitializeSetup(): Boolean;
begin
    if not IsDotNetDetected('v4\Client', 0) then begin
        MsgBox(ExpandConstant('{cm:DotNetVersionErrorMessage}'),
             mbInformation, MB_OK);
        result := false;
    end else
        result := true;
end;

