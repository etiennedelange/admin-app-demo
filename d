[33mcommit 539c596d9d641351b74c0908da6f39c18b729b7d[m[33m ([m[1;36mHEAD -> [m[1;32mmaster[m[33m, [m[1;31morigin/master[m[33m)[m
Author: EtienneDZA <etienne.de.lange1@gmail.com>
Date:   Wed Oct 21 14:53:36 2020 +0200

    Various changes
    
    Various changes
    - Adding date format config for Material datepicker
    - Adding reports component (WIP)

M	.gitignore
M	AdminApp.API/Controllers/AttorneysController.cs
M	AdminApp.API/Controllers/AuthenticationController.cs
A	AdminApp.API/Services/AuthenticationService.cs
M	AdminApp.API/Services/EmailSendingService.cs
A	AdminApp.API/Services/IAuthenticationService.cs
A	AdminApp.API/Services/IEmailSendingService.cs
M	AdminApp.API/Startup.cs
M	AdminApp.API/ViewModels/AttorneyViewModel.cs
M	AdminApp.API/ViewModels/SettingViewModel.cs
M	AdminApp.Data/DbInitializer.cs
M	AdminApp.Data/Models/Attorney.cs
M	Notes.txt
M	admin-app/package-lock.json
M	admin-app/package.json
A	admin-app/src/app/accounts/account-details/account-details.component.html
A	admin-app/src/app/accounts/account-details/account-details.component.less
A	admin-app/src/app/accounts/account-details/account-details.component.spec.ts
A	admin-app/src/app/accounts/account-details/account-details.component.ts
A	admin-app/src/app/accounts/accounts.component.less
A	admin-app/src/app/accounts/accounts.component.spec.ts
A	admin-app/src/app/accounts/accounts.component.ts
A	admin-app/src/app/accounts/accounts.module.ts
A	admin-app/src/app/accounts/accounts.routing.module.ts
M	admin-app/src/app/app-routing.module.ts
M	admin-app/src/app/app.module.ts
M	admin-app/src/app/app.shared.module.ts
M	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.html
M	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.less
M	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.ts
M	admin-app/src/app/attorneys/attorney.service.ts
M	admin-app/src/app/attorneys/attorneys.routing.module.ts
M	admin-app/src/app/common/app-form-field/app-form-field.component.html
A	admin-app/src/app/date-format-configuration.ts
M	admin-app/src/app/header/app-header.component.html
M	admin-app/src/app/nav-menu/app-nav-menu.component.html
A	admin-app/src/app/reports/report-list/report-list.component.html
A	admin-app/src/app/reports/report-list/report-list.component.less
A	admin-app/src/app/reports/report-list/report-list.component.spec.ts
A	admin-app/src/app/reports/report-list/report-list.component.ts
A	admin-app/src/app/reports/reports.component.less
A	admin-app/src/app/reports/reports.component.spec.ts
A	admin-app/src/app/reports/reports.component.ts
A	admin-app/src/app/reports/reports.module.ts
A	admin-app/src/app/reports/reports.routing.module.ts
M	admin-app/src/app/settings/setting-detail.component.ts
M	admin-app/src/app/settings/setting-list-resolver.service.ts
M	admin-app/src/app/users/user-detail.component.html
M	admin-app/src/app/users/user-detail.component.ts
M	admin-app/src/styles.less

[33mcommit 8c0f5e5cfaa6e4c10805b4b6998a0c0ffaa5ad43[m
Author: EtienneDZA <etienne.de.lange1@gmail.com>
Date:   Wed Sep 30 14:07:44 2020 +0200

    Removing vscode specific files

D	.vscode/launch.json
D	.vscode/tasks.json

[33mcommit 8bd4973ea6884852936b47c5645f73dfb3891d10[m
Author: EtienneDZA <etienne.de.lange1@gmail.com>
Date:   Wed Sep 30 14:04:19 2020 +0200

    Comitting initial files
    
    Comitting initial files

A	.gitattributes
A	.gitignore
A	.vscode/launch.json
A	.vscode/tasks.json
A	AdminApp.API/.editorconfig
A	AdminApp.API/AdminApp.API.csproj
A	AdminApp.API/AppSettings.cs
A	AdminApp.API/Controllers/ApiControllerBase.cs
A	AdminApp.API/Controllers/AttorneysController.cs
A	AdminApp.API/Controllers/AuthenticationController.cs
A	AdminApp.API/Controllers/SettingsController.cs
A	AdminApp.API/Controllers/UsersController.cs
A	AdminApp.API/GlobalSuppressions.cs
A	AdminApp.API/Helpers/EmailTemplateHelper.cs
A	AdminApp.API/Program.cs
A	AdminApp.API/Properties/PublishProfiles/FolderProfile.pubxml
A	AdminApp.API/Properties/launchSettings.json
A	AdminApp.API/Resources/LexisConvey.png
A	AdminApp.API/Resources/LexisNexis.jpg
A	AdminApp.API/Resources/Templates/ActivationEmailTemplate.cshtml
A	AdminApp.API/ServiceExtensions.cs
A	AdminApp.API/Services/Authentication.cs
A	AdminApp.API/Services/EmailSendingService.cs
A	AdminApp.API/Services/IAuthentication.cs
A	AdminApp.API/Startup.cs
A	AdminApp.API/ViewModels/AttorneyViewModel.cs
A	AdminApp.API/ViewModels/AuthenticationUserViewModel.cs
A	AdminApp.API/ViewModels/LoginViewModel.cs
A	AdminApp.API/ViewModels/RegisterViewModel.cs
A	AdminApp.API/ViewModels/SettingViewModel.cs
A	AdminApp.API/ViewModels/UserViewModel.cs
A	AdminApp.API/appsettings.Development.json
A	AdminApp.API/appsettings.json
A	AdminApp.API/web.config
A	AdminApp.Data/AdminApp.Data.csproj
A	AdminApp.Data/AdminAppDbContext.cs
A	AdminApp.Data/DbInitializer.cs
A	AdminApp.Data/Migrations/20200717083129_InitialCreate.Designer.cs
A	AdminApp.Data/Migrations/20200717083129_InitialCreate.cs
A	AdminApp.Data/Migrations/20200717144452_AddIsHostedFirmColumn.Designer.cs
A	AdminApp.Data/Migrations/20200717144452_AddIsHostedFirmColumn.cs
A	AdminApp.Data/Migrations/20200828131355_SingularizeTableNames.Designer.cs
A	AdminApp.Data/Migrations/20200828131355_SingularizeTableNames.cs
A	AdminApp.Data/Migrations/AdminAppDbContextModelSnapshot.cs
A	AdminApp.Data/Models/ApplicationRole.cs
A	AdminApp.Data/Models/ApplicationUser.cs
A	AdminApp.Data/Models/Attorney.cs
A	AdminApp.Data/Models/AttorneySetting.cs
A	AdminApp.Data/Models/DomainModel.cs
A	AdminApp.Data/Models/Error.cs
A	AdminApp.Data/Models/JobTitle.cs
A	AdminApp.Data/Models/LoggedEvent.cs
A	AdminApp.Data/Models/Setting.cs
A	AdminApp.Data/Scripts/1. CreateDatabase.sql
A	AdminApp.Data/Scripts/3. SeedDatabase.sql
A	AdminApp.Data/Scripts/CreateUser.sql
A	AdminApp.sln
A	Notes.txt

[33mcommit 3060047427484fbe9c720e725b80091599be0c8c[m
Author: EtienneDZA <etienne.de.lange1@gmail.com>
Date:   Wed Sep 30 14:01:29 2020 +0200

    Adding admin app initial files

A	admin-app/admin-app-workspace.code-workspace
A	admin-app/angular.json
A	admin-app/e2e/protractor.conf.js
A	admin-app/e2e/src/app.e2e-spec.ts
A	admin-app/e2e/src/app.po.ts
A	admin-app/e2e/tsconfig.json
A	admin-app/karma.conf.js
A	admin-app/out-tsc/e2e/app.e2e-spec.js
A	admin-app/out-tsc/e2e/app.e2e-spec.js.map
A	admin-app/out-tsc/e2e/app.po.js
A	admin-app/out-tsc/e2e/app.po.js.map
A	admin-app/package-lock.json
A	admin-app/package.json
A	admin-app/src/app/admin/admin.component.html
A	admin-app/src/app/admin/admin.component.ts
A	admin-app/src/app/animations.ts
A	admin-app/src/app/app-routing.module.ts
A	admin-app/src/app/app.component.html
A	admin-app/src/app/app.component.less
A	admin-app/src/app/app.component.spec.ts
A	admin-app/src/app/app.component.ts
A	admin-app/src/app/app.material.module.ts
A	admin-app/src/app/app.module.ts
A	admin-app/src/app/app.shared.module.ts
A	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.html
A	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.less
A	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.spec.ts
A	admin-app/src/app/attorneys/attorney-detail-component/attorney-detail.component.ts
A	admin-app/src/app/attorneys/attorney-detail-dialog-component/attorney-detail-dialog.component.html
A	admin-app/src/app/attorneys/attorney-detail-dialog-component/attorney-detail-dialog.component.less
A	admin-app/src/app/attorneys/attorney-detail-dialog-component/attorney-detail-dialog.component.ts
A	admin-app/src/app/attorneys/attorney-list.component.html
A	admin-app/src/app/attorneys/attorney-list.component.less
A	admin-app/src/app/attorneys/attorney-list.component.ts
A	admin-app/src/app/attorneys/attorney.service.ts
A	admin-app/src/app/attorneys/attorneys-list-resolver.service.ts
A	admin-app/src/app/attorneys/attorneys.component.ts
A	admin-app/src/app/attorneys/attorneys.module.ts
A	admin-app/src/app/attorneys/attorneys.routing.module.ts
A	admin-app/src/app/auth-guard.service.ts
A	admin-app/src/app/common/add-button/app-add-button.component.html
A	admin-app/src/app/common/add-button/app-add-button.component.less
A	admin-app/src/app/common/add-button/app-add-button.component.spec.ts
A	admin-app/src/app/common/add-button/app-add-button.component.ts
A	admin-app/src/app/common/app-form-field/app-form-field.component.html
A	admin-app/src/app/common/app-form-field/app-form-field.component.less
A	admin-app/src/app/common/app-form-field/app-form-field.component.spec.ts
A	admin-app/src/app/common/app-form-field/app-form-field.component.ts
A	admin-app/src/app/common/app-loading-bar/loading-bar.component.html
A	admin-app/src/app/common/app-loading-bar/loading-bar.component.less
A	admin-app/src/app/common/app-loading-bar/loading-bar.component.spec.ts
A	admin-app/src/app/common/app-loading-bar/loading-bar.component.ts
A	admin-app/src/app/header/app-header.component.html
A	admin-app/src/app/header/app-header.component.less
A	admin-app/src/app/header/app-header.component.ts
A	admin-app/src/app/http-interceptors.ts
A	admin-app/src/app/login/credentials.interface.ts
A	admin-app/src/app/login/login-routing.module.ts
A	admin-app/src/app/login/login.component.html
A	admin-app/src/app/login/login.component.less
A	admin-app/src/app/login/login.component.ts
A	admin-app/src/app/nav-menu/app-nav-menu.component.html
A	admin-app/src/app/nav-menu/app-nav-menu.component.less
A	admin-app/src/app/nav-menu/app-nav-menu.component.ts
A	admin-app/src/app/page-not-found/page-not-found.component.html
A	admin-app/src/app/page-not-found/page-not-found.component.less
A	admin-app/src/app/page-not-found/page-not-found.component.spec.ts
A	admin-app/src/app/page-not-found/page-not-found.component.ts
A	admin-app/src/app/settings/setting-detail.component.html
A	admin-app/src/app/settings/setting-detail.component.less
A	admin-app/src/app/settings/setting-detail.component.ts
A	admin-app/src/app/settings/setting-list-resolver.service.ts
A	admin-app/src/app/settings/setting-list.component.html
A	admin-app/src/app/settings/setting-list.component.less
A	admin-app/src/app/settings/setting-list.component.ts
A	admin-app/src/app/settings/setting.service.ts
A	admin-app/src/app/settings/settings.component.ts
A	admin-app/src/app/settings/settings.module.ts
A	admin-app/src/app/settings/settings.routing.module.ts
A	admin-app/src/app/shared/services/authentication.interceptor.ts
A	admin-app/src/app/shared/services/authentication.service.ts
A	admin-app/src/app/shared/services/error.interceptor.ts
A	admin-app/src/app/shared/services/loading-bar.interceptor.spec.ts
A	admin-app/src/app/shared/services/loading-bar.interceptor.ts
A	admin-app/src/app/shared/services/loading-bar.service.spec.ts
A	admin-app/src/app/shared/services/loading-bar.service.ts
A	admin-app/src/app/shared/services/snack-bar.service.spec.ts
A	admin-app/src/app/shared/services/snack-bar.service.ts
A	admin-app/src/app/users/user-activation/user-activation.component.html
A	admin-app/src/app/users/user-activation/user-activation.component.less
A	admin-app/src/app/users/user-activation/user-activation.component.spec.ts
A	admin-app/src/app/users/user-activation/user-activation.component.ts
A	admin-app/src/app/users/user-detail.component.html
A	admin-app/src/app/users/user-detail.component.less
A	admin-app/src/app/users/user-detail.component.ts
A	admin-app/src/app/users/user-list-resolver.service.ts
A	admin-app/src/app/users/user-list.component.html
A	admin-app/src/app/users/user-list.component.less
A	admin-app/src/app/users/user-list.component.ts
A	admin-app/src/app/users/user.component.ts
A	admin-app/src/app/users/user.module.ts
A	admin-app/src/app/users/user.routing.module.ts
A	admin-app/src/app/users/user.service.ts
A	admin-app/src/assets/GhostConvey-orig.png
A	admin-app/src/environments/environment.prod.ts
A	admin-app/src/environments/environment.ts
A	admin-app/src/favicon.ico
A	admin-app/src/index.html
A	admin-app/src/main.ts
A	admin-app/src/material-app-theme.scss
A	admin-app/src/polyfills.ts
A	admin-app/src/styles.css
A	admin-app/src/styles.less
A	admin-app/src/styles.scss
A	admin-app/src/variables.less
A	admin-app/src/vendor.ts
A	admin-app/tsconfig.app.json
A	admin-app/tsconfig.json
A	admin-app/tsconfig.spec.json
A	admin-app/tslint.json
