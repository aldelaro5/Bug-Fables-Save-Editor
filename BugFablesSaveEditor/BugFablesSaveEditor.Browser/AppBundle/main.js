import {dotnet} from './dotnet.js'

const is_browser = typeof window != "undefined";
if (!is_browser) throw new Error(`Expected to be running in a browser`);

const dotnetRuntime = await dotnet
  .withDiagnosticTracing(false)
  .withApplicationArgumentsFromQuery()
  .create();

dotnetRuntime.setModuleImports("main.js", {
  ShowMessageBoxAsync: async (title, message, icon, buttonsType) => {
    const result = await Swal.fire({
      title: title,
      text: message,
      icon: icon,
      customClass: 'message-box',
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCloseButton: false,
      showCancelButton: false,
      showDenyButton: buttonsType === 'yesNo',
      confirmButtonText: buttonsType === 'yesNo' ? 'Yes' : 'Ok',
      denyButtonText: 'No',
    });
    return result.isConfirmed;
  },
  DownloadSaveFileAsync: async (base64Data, fileName) => {
    const bytes = new Uint8Array(atob(base64Data).split('').map(x => x.charCodeAt(0)))
    const blob = new Blob([bytes], {type: "text/plain;charset=utf-8"});
    const link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    link.download = fileName;
    link.click();
    window.URL.revokeObjectURL(link.href);
  },
});

const config = dotnetRuntime.getConfig();

await dotnetRuntime.runMainAndExit(config.mainAssemblyName, [window.location.search]);
