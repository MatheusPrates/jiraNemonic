var view = 1;
var issues = 0;
var num_issues = 0;


var myFunction = function (teste) {
    //console.log(teste);
    var keys = teste.keys;
    var storypoints = teste.storypoints;
    var summaries = teste.summaries;
    var labels = teste.labels;
    var messages = teste.message;
    if (messages.length > 0)
    {
        alert(messages);
    }

    if (keys.length == 0)
    {
        return;
    }

    for (i = 0; i < keys.length; i++) {
        key = keys[i];
        storyp = storypoints[i];
        summary = summaries[i];
        label = labels[i];


        root = document.createElement("div");
        root.classList.add("issue");


        label_tag = document.createElement("h1");
        label_tag.classList.add("issue_label");
        label_tag.textContent = label;


        summary_tag = document.createElement("h1");
        summary_tag.classList.add("issue_summary");
        summary_tag.textContent = summary;


        story_points_div_tag = document.createElement("div");
        story_points_div_tag.classList.add("storyp");
        story_points_h1_tag = document.createElement("h1");
        story_points_h1_tag.textContent = "SP: " + storyp;
        story_points_div_tag.appendChild(story_points_h1_tag);

        key_div_tag = document.createElement("div");
        key_div_tag.classList.add("issuekey");
        key_h1_tag = document.createElement("h1");
        key_h1_tag.textContent = key;
        key_div_tag.appendChild(key_h1_tag);


        bocom_logo_tag = document.createElement("img");
        bocom_logo_tag.classList.add("bocombbmlogo");
        bocom_logo_tag.src = "../Images/bocombbm.jpg";
        // bocom_logo_tag.setAttribute("src","../Extras/bocombbm.jpg");

        root.appendChild(label_tag);
        root.appendChild(summary_tag);
        root.appendChild(story_points_div_tag);
        root.appendChild(key_div_tag);
        root.appendChild(bocom_logo_tag);

        patriarch = document.querySelector(".allIssues");
        //console.log(patriarch);
        patriarch.appendChild(root);

        //console.log(patriarch);
    }
}

var switchViews = function () {

    sprint = document.querySelector("#queryAll");
    issue = document.querySelector("#queryIssues");
    if (view == 1) {
        sprint.classList.add("invisivel");
        issue.classList.remove("invisivel");
    }
    else {
        issue.classList.add("invisivel");
        sprint.classList.remove("invisivel");
    }


    view = 1 - view;



}

var deleteIssue = function (btnId) {
    console.log(btnId.parentNode.childNodes[1].value);
    btnId.parentNode.classList.add("invisivel");
    btnId.parentNode.childNodes[0].childNodes.value = "";
    btnId.parentNode.childNodes[1].value = "";
    num_issues--;
}

var addIssue = function()
{

    form = document.querySelector("#issueQuery");

    centeronpage = document.createElement("div");
    centeronpage.classList.add("center-on-page");

    select = document.createElement("div");
    select.classList.add("select");

    combobox = document.createElement("select");
    combobox.name = "project["+(issues)+"]";
    combobox.id = "slct";

    option0 = document.createElement("option");
    option0.textContent = "Choose a project";

    option1 = document.createElement("option");
    option1.value = "TARGARYENS";
    option1.textContent = "TARGARYENS";

    option2 = document.createElement("option");
    option2.value = "STARKS";
    option2.textContent = "STARKS";

    option3 = document.createElement("option");
    option3.value = "TA";
    option3.textContent = "AVENGERS";
    
    option4 = document.createElement("option");
    option4.value = "TTDE";
    option4.textContent = "TROPA DE ELITE";

    option5 = document.createElement("option");
    option5.value = "TD";
    option5.textContent = "DEVOPS";

    option6 = document.createElement("option");
    option6.value = "BOPE";
    option6.textContent = "BOPE";

    console.log(option6);

    sprintnum = document.createElement("input");
    sprintnum.id = "snum";
    sprintnum.name = "sprint[" + (issues) + "]";
    sprintnum.type = "text";
    sprintnum.placeholder = "Issue Key - Ex: 689,717";

    deleterButton = document.createElement("button");
    deleterButton.classList.add("btn");
    deleterButton.classList.add("btn-default");
    deleterButton.id="b_"+issues;
    deleterButton.type="button";

    deleterButton.textContent = "Delete";

    var idbotao = 'b_' + issues;
    console.log(idbotao);
    deleterButton.setAttribute("onclick", "deleteIssue(" + idbotao + ")");
    if(issues==0)
        deleterButton.style = "position:relative;left:7%;top:15px;";
    else
        deleterButton.style = "position:relative;left:7%;top:15px;margin-top:48px;";

    combobox.appendChild(option0);
    combobox.appendChild(option1);
    combobox.appendChild(option2);
    combobox.appendChild(option3);
    combobox.appendChild(option4);
    combobox.appendChild(option5);
    combobox.appendChild(option6);

    select.appendChild(combobox);


    centeronpage.classList.add("dynamicIssue");
    centeronpage.appendChild(select);
    centeronpage.appendChild(sprintnum);
    centeronpage.appendChild(deleterButton);

    
    buttons = document.querySelector("#post_issues");

    form.insertBefore(centeronpage,buttons);

    issues++;
    num_issues++;
}

var removeAllIssues = function () {
    rows = document.querySelectorAll(".dynamicIssue");
    rows.forEach(function (row) {
        row.remove();
    });
    issues = 0;

}
