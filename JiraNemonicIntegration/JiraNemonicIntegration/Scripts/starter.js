console.log("HIasasU");


var myFunction = function (teste) {
    //console.log(teste);
    var keys = teste.keys;
    var storypoints = teste.storypoints;
    var summaries = teste.summaries;
    var labels = teste.labels;

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