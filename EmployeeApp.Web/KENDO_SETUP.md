# Kendo UI Setup Guide

This guide explains how to set up Kendo UI for jQuery in the EmployeeApp.Web project.

## 📦 Prerequisites

- Telerik account (free trial available)
- ~210 MB disk space for Kendo UI files

## 🚀 Quick Setup

### Step 1: Download Kendo UI

1. **Visit Telerik Downloads:**
   ```
   https://www.telerik.com/download/kendo-ui
   ```

2. **Create Account or Login:**
   - Click "Try now" for 30-day free trial
   - Or login if you have existing account
   - Fill registration form (free trial, no credit card required)

3. **Download Kendo UI Professional:**
   - After login, go to Downloads section
   - Select **"Kendo UI for jQuery"**
   - Choose **"Commercial"** version (includes Grid popup editing)
   - Download format: **ZIP file** (~200 MB)

### Step 2: Extract Files

1. **Unzip downloaded file:**
   ```bash
   unzip ~/Downloads/kendoui.professional.2026.1.212.commercial.zip
   ```

2. **Locate required folders:**
   ```
   kendoui.professional.2026.1.212.commercial/
   ├── js/
   │   └── kendo.all.min.js      # Main JavaScript file
   └── styles/
       └── default-main.css       # Default theme
   ```

### Step 3: Copy to Project

1. **Navigate to project:**
   ```bash
   cd /path/to/EmployeeApp/EmployeeApp.Web
   ```

2. **Create Kendo directory:**
   ```bash
   mkdir -p wwwroot/lib/kendo-ui/js
   mkdir -p wwwroot/lib/kendo-ui/styles
   ```

3. **Copy JavaScript files:**
   ```bash
   cp ~/Downloads/kendoui.professional.2026.1.212.commercial/js/*.js wwwroot/lib/kendo-ui/js/
   cp ~/Downloads/kendoui.professional.2026.1.212.commercial/js/*.map wwwroot/lib/kendo-ui/js/
   ```

4. **Copy CSS files:**
   ```bash
   cp ~/Downloads/kendoui.professional.2026.1.212.commercial/styles/*.css wwwroot/lib/kendo-ui/styles/
   ```

### Step 4: Get License Key

1. **Go to Telerik License Page:**
   ```
   https://www.telerik.com/account/product-keys
   ```

2. **Find your license key:**
   - Login with your account
   - Look for "Kendo UI for jQuery"
   - Copy the **Script License Key** (long alphanumeric string)

3. **Create license file:**
   ```bash
   cd EmployeeApp.Web
   nano wwwroot/js/kendo-ui-license.js
   ```

4. **Add this content (replace YOUR_LICENSE_KEY):**
   ```javascript
   KendoLicensing.setScriptKey("YOUR_LICENSE_KEY_HERE");
   ```

   Example:
   ```javascript
   KendoLicensing.setScriptKey("141j044b041h541j4i1d542e58285k264c22502j5f465d38573958385b3557315e472j48...");
   ```

### Step 5: Verify Installation

1. **Check files exist:**
   ```bash
   ls wwwroot/lib/kendo-ui/js/kendo.all.min.js
   ls wwwroot/lib/kendo-ui/styles/default-main.css
   ls wwwroot/js/kendo-ui-license.js
   ```

2. **Check file sizes:**
   ```bash
   du -sh wwwroot/lib/kendo-ui/
   # Should show ~210M
   ```

## 🎯 Alternative: Using CDN (Not Recommended)

If you prefer CDN, edit `Views/Shared/_Layout.cshtml`:

```html
<!-- Replace local references with CDN -->
<link rel="stylesheet" href="https://kendo.cdn.telerik.com/themes/13.0.0/default/default-main.css" />
<script src="https://code.jquery.com/jquery-3.7.0.min.js"></script>
<script src="https://kendo.cdn.telerik.com/2026.1.212/js/kendo.all.min.js"></script>

<!-- You still need license file -->
<script src="~/js/kendo-ui-license.js"></script>
```

**Note:** Local files are recommended for:
- Faster load times
- Offline development
- Version control

## ✅ Verification

1. **Start the application:**
   ```bash
   dotnet run
   ```

2. **Open browser:**
   ```
   http://localhost:5142
   ```

3. **Navigate to Employees page:**
   - Click "Go to Employee Grid"
   - You should see Kendo Grid with data

4. **Check browser console:**
   - Press `F12` → Console tab
   - Should have NO license warnings
   - Should have NO 404 errors for kendo files

## 🐛 Troubleshooting

### License Warning Appears

**Problem:** "No license found for Kendo UI"

**Solutions:**
1. Verify license file exists: `wwwroot/js/kendo-ui-license.js`
2. Check license key is correct (no spaces or line breaks)
3. Ensure license file is loaded in `_Layout.cshtml` AFTER `kendo.all.min.js`
4. Clear browser cache (Ctrl+Shift+R)

### Grid Not Showing

**Problem:** Grid container is empty

**Solutions:**
1. Check browser console for errors
2. Verify files exist:
   ```bash
   ls wwwroot/lib/kendo-ui/js/kendo.all.min.js
   ls wwwroot/lib/kendo-ui/styles/default-main.css
   ```
3. Check Network tab in DevTools for 404 errors
4. Ensure API is running at http://localhost:5080

### 404 Errors for Kendo Files

**Problem:** Browser shows 404 for kendo.all.min.js

**Solutions:**
1. Verify files copied correctly
2. Check file permissions:
   ```bash
   chmod -R 644 wwwroot/lib/kendo-ui/
   ```
3. Restart the application: `dotnet run`

### Trial License Expired

**Problem:** "Your trial period has expired"

**Solutions:**
1. **Option 1:** Purchase commercial license from Telerik
2. **Option 2:** Switch to Kendo UI Core (GPL, free but limited features)
3. **Option 3:** Request trial extension from Telerik support

## 📚 File Structure After Setup

```
EmployeeApp.Web/
└── wwwroot/
    ├── js/
    │   └── kendo-ui-license.js          # Your license key
    └── lib/
        └── kendo-ui/                     # ~210 MB
            ├── js/                       # JavaScript files
            │   ├── kendo.all.min.js      # Main file (required)
            │   ├── kendo.all.min.js.map
            │   ├── kendo.core.min.js
            │   ├── kendo.grid.min.js
            │   └── ... (other components)
            └── styles/                   # CSS themes
                ├── default-main.css      # Current theme
                ├── default-main-dark.css
                ├── bootstrap-main.css
                ├── material-main.css
                └── ... (other themes)
```

## 🎨 Using Different Themes

To change the theme, edit `Views/Shared/_Layout.cshtml`:

```html
<!-- Default theme (current) -->
<link rel="stylesheet" href="~/lib/kendo-ui/styles/default-main.css" />

<!-- OR Bootstrap theme -->
<link rel="stylesheet" href="~/lib/kendo-ui/styles/bootstrap-main.css" />

<!-- OR Material theme -->
<link rel="stylesheet" href="~/lib/kendo-ui/styles/material-main.css" />

<!-- OR Dark mode -->
<link rel="stylesheet" href="~/lib/kendo-ui/styles/default-main-dark.css" />
```

## 🔗 Resources

- **Kendo UI Documentation:** https://docs.telerik.com/kendo-ui/introduction
- **Grid Documentation:** https://docs.telerik.com/kendo-ui/controls/grid/overview
- **Download Page:** https://www.telerik.com/download/kendo-ui
- **License Keys:** https://www.telerik.com/account/product-keys
- **Support:** https://www.telerik.com/account/support-tickets

## 💡 Version Information

This project uses:
- **Kendo UI:** 2026.1.212 (Professional/Commercial)
- **jQuery:** 3.7.0
- **License:** 30-day trial (can be extended or purchased)

## 📝 Notes

1. **Do NOT commit Kendo files to git** - They are ignored in `.gitignore`
2. **Each developer needs to download** Kendo separately
3. **License key is personal** - Each developer should get their own
4. **Free trial is 30 days** - Plan accordingly for production
5. **Commercial use requires license** - Contact Telerik for pricing

## ✨ What's Next?

After setup:
1. ✅ Verify grid loads: http://localhost:5142/Employee
2. ✅ Test CRUD operations (Create, Edit, Delete)
3. ✅ Explore sorting, filtering, pagination
4. 📖 Read Kendo Grid docs for advanced features
