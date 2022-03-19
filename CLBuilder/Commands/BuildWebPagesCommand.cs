using CLBuilder.viewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLBuilder.Commands
{
    public class BuildWebPagesCommand : BaseCommand
    {
        private readonly string cssText = @"/* By Andrea Maselli https://codepen.io/andrea-maselli */
/* MIT license */
.btn {
  border: none;
  font-family: 'Lato';
  font-size: inherit;
  color: inherit;
  background: none;
  cursor: pointer;
  padding: 25px 80px;
  display: inline-block;
  margin: 5px 10px;
  text-transform: uppercase;
  letter-spacing: 1px;
  font-weight: 700;
  width: 350px;
  outline: none;
  position: relative;
  -webkit-transition: all 0.3s;
  -moz-transition: all 0.3s;
  transition: all 0.3s;
}

.btn:after {
  content: '';
  position: absolute;
  z-index: -1;
  -webkit-transition: all 0.3s;
  -moz-transition: all 0.3s;
  transition: all 0.3s;
}

/* Button 1 */
.btn-1 {
    background: #3498db;
    color: #fff;
}

.btn-1:hover {
  background: #2980b9;
}

.btn-1:active {
  background: #2980b9;
  top: 2px;
}

.btn-1:before {
  position: absolute;
  height: 100%;
  left: 0;
  top: 0;
  line-height: 3;
  font-size: 140%;
  width: 60px;
}

/* Button 2 */
.btn-2 {
  background: #2ecc71;
  color: #fff;
}

.btn-2:hover {
  background: #27ae60;
}

.btn-2:active {
  background: #27ae60;
  top: 2px;
}

.btn-2:before {
  position: absolute;
  height: 100%;
  left: 0;
  top: 0;
  line-height: 3;
  font-size: 140%;
  width: 60px;
}

/* Button 3 */
.btn-3 {
  background: #e74c3c;
  color: #fff;
}

.btn-3:hover {
  background: #c0392b;
}

.btn-3:active {
  background: #c0392b;
  top: 2px;
}

.btn-3:before {
  position: absolute;
  height: 100%;
  left: 0;
  top: 0;
  line-height: 3;
  font-size: 140%;
  width: 60px;
}

/* Button 3 */
.btn-4 {
  background: #34495e;
  color: #fff;
}

.btn-4:hover {
  background: #2c3e50;
}

.btn-4:active {
  background: #2c3e50;
  top: 2px;
}

.btn-4:before {
  position: absolute;
  height: 100%;
  left: 0;
  top: 0;
  line-height: 3;
  font-size: 140%;
  width: 60px;
}";
        private readonly string caseTemplate = @"                                case {0}:
                                    nextUrl = nextUrl + ""{0}_{1}.html"";
                                    break;
";
        private readonly string control = @"    var AAO_URL = ""<AAO_URL>"";

    var currentChecklist = 0;

        var xhttp = new XMLHttpRequest();
        xhttp.addEventListener(""load"", dataRequestListener);

    var mainLoopRequestObj = {2}{3};
        mainLoopRequestObj.getvars = [];
    var toget = {2} ""var"": ""(L:clphase)"", ""value"": 0 {3};
        mainLoopRequestObj.getvars.push(toget);
    toget = {2} ""var"": ""(L:A320Checklist)"", ""value"": 0 {3};
    mainLoopRequestObj.getvars.push(toget);

    var lastItem;
        var lastLabel;

        function Reset()
        {2}
            lastItem = null;
            lastLabel = null;
            for (let i = 1; i < 50; i++)
            {2}
                let itemId = ""item_"" + i.toString();
                var cbs = document.getElementById(itemId);
                if (cbs)
                {2}
                    cbs.checked = false;
                    {3} else
                    {2}
                        return;
                    {3}
                {3}
            {3};

            // Main data loop to gather feedback from AAO
            function DataLoop()
            {2}
                if (xhttp != null)
                {2}
                    xhttp.open(""GET"", AAO_URL + ""?json="" + JSON.stringify(mainLoopRequestObj));
                    xhttp.send();
                {3}
            {3};

            // Process the received values
            function dataRequestListener()
            {2}
                var commObj = JSON.parse(this.responseText);
                if (commObj.getvars[0].value == 0)
                {2}
                    if (lastItem)
                    {2}
                        document.getElementById(lastItem).checked = true;
                        document.getElementById(lastLabel).style.fontWeight = ""normal"";
                        document.getElementById(lastLabel).style.fontSize = ""24px"";

                        {3}
                    {3}
                    else
                    {2}
                        let itemId = ""item_"" + commObj.getvars[0].value.toString();
                        if (!lastItem || lastItem !== itemId)
                        {2}
                            let labelId = ""itemLabel_"" + commObj.getvars[0].value.toString();
                            if (lastLabel && lastLabel !== labelId)
                            {2}
                                document.getElementById(lastItem).checked = true;
                                document.getElementById(lastLabel).style.fontWeight = ""normal"";
                                document.getElementById(lastLabel).style.fontSize = ""24px"";

                                {3}
                                lastItem = itemId;
                                lastLabel = labelId;
                                document.getElementById(labelId).style.fontWeight = ""bold"";
                                document.getElementById(lastLabel).style.fontSize = ""28px"";
                            {3}
                        {3}
                        if (commObj.getvars[1].value != currentChecklist)
                        {2}
                            currentChecklist = commObj.getvars[1].value;
                            let nextUrl = ""<AAO_URL>"" + ""/{0}/"";
                            switch (currentChecklist)
                            {2}
                                case 1:
                                    nextUrl = nextUrl + ""index.html"";
                                    break;
{1}
                                default:
                                    nextUrl = nextUrl + ""index.html"";
                            {3}
                            window.location.href = nextUrl;
                        {3}
                    {3};

                    // Data loop runs every 250 milliseconds 
                    setInterval(DataLoop, 250);

                    // Simulator event that is processed directly by AAO
                    function sendEvent(val, evt)
                    {2}
                        var requestObj = {2}{3};
                        requestObj.triggers = [];
                        var tosend = {2} ""evt"": evt, ""value"": val{3};
        requestObj.triggers.push(tosend);
        var relxhttp = new XMLHttpRequest();
        var relurl = encodeURI(AAO_URL + ""?json="" + JSON.stringify(requestObj));
        relurl = relurl.replace(/\+/g, '%2B');
        relxhttp.open(""POST"", relurl);
        relxhttp.send();
    {3}

    // RPN scripts to be processed by AAO
    function sendScript(scr)
    {2}
        var requestObj = {2}{3};
        requestObj.scripts = [];
        var tosend = {2} ""code"": scr{3};
    requestObj.scripts.push(tosend);
        var relxhttp = new XMLHttpRequest();
    var relurl = encodeURI(AAO_URL + ""?json="" + JSON.stringify(requestObj));
    relurl = relurl.replace(/\+/g, '%2B');
        relxhttp.open(""POST"", relurl);
        relxhttp.send();
    {3}

function itemCheckClick(itemId)
{2}
    if (lastItem && itemId === lastItem)
    {2}
        document.getElementById(itemId).checked = false;
        sendEvent(1, '(>K:AAO_CL_CHECKED)');
        {3}
    {3}

    function bStopClClick()
    {2}
        sendScript('1 (>K:AAO_CL_STOP) 0 (>L:clphase)');
        Reset();
    {3}";

        private readonly MainViewModel mainViewModel;

        public BuildWebPagesCommand(MainViewModel mainViewModel)
        {
            this.mainViewModel = mainViewModel;
            this.mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;
            if(this.mainViewModel.ChecklistControlViewModel != null)
            {
                this.mainViewModel.ChecklistControlViewModel.PropertyChanged += ChecklistControlViewModel_PropertyChanged;
            }
        }

        public override bool CanExecute(object parameter)
        {
            try
            {
                var root = mainViewModel.ChecklistControlViewModel.InstallFolder;
                var dir = mainViewModel.ChecklistControlViewModel.AircraftShortName;
                var invalidPathChars = Path.GetInvalidPathChars();

                if (string.IsNullOrEmpty(root) ||
                    root.Any(x => invalidPathChars.Contains(x)) ||
                    !Directory.Exists(root) ||
                    string.IsNullOrEmpty(dir) ||
                    dir.Any(x => invalidPathChars.Contains(x)))
                {
                    return false;
                }

                foreach (var cl in mainViewModel.ChecklistControlViewModel.Checklists)
                {
                    var name = cl.Name;
                    if (name.Any(m => invalidPathChars.Contains(m)))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public override void Execute(object parameter)
        {
            if (!mainViewModel.CanContinue())
            {
                return;
            }

            var root = Path.Combine(mainViewModel.ChecklistControlViewModel.InstallFolder, "WebPages");
            var icao = mainViewModel.ChecklistControlViewModel.AircraftShortName;
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            root = Path.Combine(root, icao);

            var controlModel = mainViewModel.ChecklistControlViewModel.Store();

            if (Directory.Exists(root))
            {
                // Delete directory and its contents
                Directory.Delete(root, true);
            }


            // Create a new directory for the files
            Directory.CreateDirectory(root);

            if(controlModel.Checklists.Count==0)
            {
                return;
            }

            // Create the index.html file
            var filename = "index.html";
            var text = controlModel.Checklists[0].WebText(1, controlModel.Checklists.Count != 1);
            File.WriteAllText(Path.Combine(root, filename), text);

            var caseList = new StringBuilder();
            // Write each of the checklist web files in the same way
            for (var i = 1; i < controlModel.Checklists.Count; i++)
            {
                filename = $"{i+1}_{controlModel.Checklists[i].Name}.html";
                text = controlModel.Checklists[i].WebText(i+1, i < controlModel.Checklists.Count - 1);
                File.WriteAllText(Path.Combine(root, filename), text);

                caseList.Append(string.Format(caseTemplate, i + 1, controlModel.Checklists[i].Name));
            }

            // Write the support files
            filename = "iconbuttons.css";
            File.WriteAllText(Path.Combine(root, filename), cssText);

            text = string.Format(control, controlModel.AircraftShortName, caseList, "{", "}");
            filename = "clcontrol.js";
            File.WriteAllText(Path.Combine(root, filename), text);

            text = $"http://localhost:9080/webapi/{controlModel.AircraftShortName}cl/index.html";
            File.WriteAllText(Path.Combine(root, "readme.txt"), text);
        }

        private void ChecklistControlViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }

        private void MainViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ChecklistControlViewModel")
            {
                this.mainViewModel.ChecklistControlViewModel.PropertyChanged += ChecklistControlViewModel_PropertyChanged;
            }

            OnCanExecuteChanged();
        }
    }
}
