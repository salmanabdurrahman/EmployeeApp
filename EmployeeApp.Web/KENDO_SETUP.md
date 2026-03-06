# Kendo UI Setup (EmployeeApp.Web)

This project uses **Kendo UI for jQuery** with local static files.
Kendo assets and license key are excluded from git, so every developer must set them up locally.

## What Must Exist

Required files:
- `wwwroot/lib/kendo-ui/js/kendo.all.min.js`
- `wwwroot/lib/kendo-ui/styles/default-main.css`
- `wwwroot/js/kendo-ui-license.js`

Without these files, the Employee Grid will not render.

## Prerequisites

- Telerik account (trial or paid)
- Kendo UI for jQuery download package
- Around 200 MB free disk space

Download page:
- `https://www.telerik.com/download/kendo-ui`

License keys page:
- `https://www.telerik.com/account/product-keys`

## Setup Steps

### 1. Download Kendo UI for jQuery

1. Sign in to Telerik.
2. Download `Kendo UI for jQuery` (ZIP package).
3. Extract it to any local path.

Example extracted folder:

```text
~/Downloads/kendoui.professional.x.y.z.commercial/
├── js/
└── styles/
```

### 2. Copy assets into this project

From project root:

```bash
cd EmployeeApp.Web
mkdir -p wwwroot/lib/kendo-ui/js wwwroot/lib/kendo-ui/styles
```

Set your extracted Kendo folder:

```bash
KENDO_SRC=~/Downloads/kendoui.professional.2026.1.212.commercial
```

Copy JS + theme files:

```bash
cp -R "$KENDO_SRC/js/." wwwroot/lib/kendo-ui/js/
cp -R "$KENDO_SRC/styles/." wwwroot/lib/kendo-ui/styles/
```

### 3. Add your license key

Create `wwwroot/js/kendo-ui-license.js`:

```javascript
KendoLicensing.setScriptKey("YOUR_SCRIPT_LICENSE_KEY");
```

Quick shell method:

```bash
cat > wwwroot/js/kendo-ui-license.js <<'LICENSE'
KendoLicensing.setScriptKey("YOUR_SCRIPT_LICENSE_KEY");
LICENSE
```

Replace `YOUR_SCRIPT_LICENSE_KEY` with your real key from Telerik account.

### 4. Verify files

```bash
ls -lh wwwroot/lib/kendo-ui/js/kendo.all.min.js
ls -lh wwwroot/lib/kendo-ui/styles/default-main.css
ls -lh wwwroot/js/kendo-ui-license.js
du -sh wwwroot/lib/kendo-ui
```

## Runtime Verification

1. Start API (`http://localhost:5080`)
2. Start Web app:

```bash
cd EmployeeApp.Web
dotnet run
```

3. Open `http://localhost:5142/Employee`
4. Open browser DevTools console:
- No Kendo license warning
- No `404` for files under `/lib/kendo-ui/`

## Changing Theme

Edit `Views/Shared/_Layout.cshtml` and switch stylesheet:

```html
<link rel="stylesheet" href="~/lib/kendo-ui/styles/default-main.css" />
```

Other common options:
- `bootstrap-main.css`
- `material-main.css`
- `default-main-dark.css`

## Troubleshooting

### "No license found for Kendo UI"

Check:
- `wwwroot/js/kendo-ui-license.js` exists
- key string is valid (no extra spaces/newlines)
- script is loaded after `kendo.all.min.js` in `_Layout.cshtml`

### 404 for `kendo.all.min.js` or theme CSS

Check:

```bash
ls wwwroot/lib/kendo-ui/js/kendo.all.min.js
ls wwwroot/lib/kendo-ui/styles/default-main.css
```

If missing, repeat copy step.

### Grid area appears but no data

Kendo loaded correctly, but API may be unavailable. Check:

```bash
curl http://localhost:5080/api/employees
```

Also verify `EmployeeApp.Web/appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5080"
  }
}
```

### Trial expired

Use one of these options:
1. Purchase/renew Telerik license
2. Request trial extension from Telerik
3. Move to a different UI stack if licensing is not suitable

## Git and Security Notes

- `wwwroot/lib/*` is excluded from git in this repo.
- `wwwroot/js/kendo-ui-license.js` is excluded from git.
- Never commit personal script license keys.

## References

- Kendo UI intro: `https://docs.telerik.com/kendo-ui/introduction`
- Grid docs: `https://docs.telerik.com/kendo-ui/controls/grid/overview`
- Telerik downloads: `https://www.telerik.com/download/kendo-ui`
- Product keys: `https://www.telerik.com/account/product-keys`
