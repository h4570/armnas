# How to develop Armnas?

### My setup of installed tools:  
- [Windows 10](https://en.wikipedia.org/wiki/Windows_10) - As development system.
- [node.js](https://nodejs.org/en/) - for frontend development (Angular) 
- [Angular CLI](https://angular.io/cli) - for frontend development 
- [VSCode](https://code.visualstudio.com/) - as frontend IDE (Angular) 
- [Visual Studio](https://visualstudio.microsoft.com/pl/vs/community/) - as backend IDE (.NET)

### My development procedure:

- Frontend: 
    - Open `frontend/web-app` directory in VSCode 
    - Install Angular dependencies **(only once)** via `npm install` 
    - Run frontend application via `ng serve` and open website through http://localhost:4200 
- Backend: 
    - Open `armnas.sln` in Visual Studio 
    - Run backend in background via **CTRL+F5**

<hr>

## Important info

Armans actively **uses bash to execute commands on the target system**. To facilitate the development of Armnas, an SSH module has been added. **There is logic in the code that checks the current system. When it is Windows, Armnas automatically tries to execute commands on target system via SSH.**  

So if you develop on Windows, you need Armbian/Raspbian ARM32 computer or Debian x64 virtual machine with SSH installed.

## How to provide SSH credentials for backend?
```
cd backend/WebApi
dotnet user-secrets set "Ssh:Username" "armnas" 
dotnet user-secrets set "Ssh:Password" "YOUR_TARGET_MACHINE_PASSWORD" 
dotnet user-secrets set "Ssh:Host" "YOUR_TARGET_MACHINE_IP" 
```

## I can't login to Armnas in development stage!
As you already know, backend is trying to resolve Linux commands via SSH, but
your database file is in your local machine, and you need to **register admin user** once.
You can do it via **Postman** or **curl** (see `install.sh`) by **POST**ing https://localhost:44370/user/register with the following body:
```
{
    "id":0,
    "login":"admin",
    "password":"YOUR_PASS"
}
```

## How to quickly deploy Armnas to target machine?
If you want to deploy your modified version of Armnas to your target machine, you can do it easily by `dev-scripts/deploy-XXX.ps1` Powershell scripts.

As first, you need to duplicate `deploy-prd-example.ps1` file and name it `deploy-prd.ps1`. After it, please enter your password in newly created file. The password should be for `armnas` user, from target machine. For your safety, the new file is NOT tracked by git!

After it you can easily run deploy script in Powershell via `./deploy-prd.ps1`.

Please be sure that you have [Powershell](https://github.com/PowerShell/PowerShell/releases) in at least 7.X.X version ( `(Get-Host).Version` )

Main differences between `dbg` and `prd` scripts:
- `dbg` - Debug
    - Faster to deploy
    - Not optimized
    - Backend build & deploy can be skipped via `-skip_backend` argument
    - Frontend build & deploy can be skipped via `-skip_frontend` argument
- `prd` - Production
    - Slower to deploy
    - Optimized
    - Backend or frontend build & deploy can't be skipped.
