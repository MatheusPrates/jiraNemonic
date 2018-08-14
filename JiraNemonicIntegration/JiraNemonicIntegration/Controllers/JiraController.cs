using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Atlassian.Jira;

namespace JiraNemonicIntegration.Controllers
{
    public class JiraController : Controller
    {

        private static void orderLists(ref List<string> sorting,ref List<string> list1,ref List<string> list2,ref List<string> list3,ref List<string> list4)
        {
            for(int i=0;i<sorting.Count;i++)
            {
                for(int j = sorting.Count-1; j>i;j--)
                {
                    if (sorting[j].CompareTo(sorting[j -1])<0)
                    {
                        string trade = sorting[j];
                        sorting[j] = sorting[j -1];
                        sorting[j -1] = trade;


                        trade = list1[j];
                        list1[j] = list1[j -1];
                        list1[j -1] = trade;

                        trade = list2[j];
                        list2[j] = list2[j - 1];
                        list2[j - 1] = trade;

                        trade = list3[j];
                        list3[j] = list3[j - 1];
                        list3[j - 1] = trade;

                        trade = list4[j];
                        list4[j] = list4[j - 1];
                        list4[j - 1] = trade;
                    }
                }
            }
        }

        private static List<List<String>> getJiraIssuesInfo(string project,string sprint,string login,string password)
        {
            var jira = Jira.CreateRestClient("https://jirabancobbm.atlassian.net", login, password);


            jira.Issues.MaxIssuesPerRequest = 2000;


            List<Issue> all_issues = new List<Issue>();

            int skipped = 0;

            List<Issue> issues = jira.Issues.Queryable.Where(i => i.Project == project && i.Type == "Story").OrderByDescending(i => i.Key).Skip(0).ToList();

            while (issues.Count > 0)
            {
                all_issues.AddRange(issues);
                skipped += 100;
                issues = jira.Issues.Queryable.Where(i => i.Project == project && i.Type == "Story").OrderByDescending(i => i.Key).Skip(skipped).ToList();
            }

            List<String> keys = new List<String>();
            List<String> labels = new List<String>();
            List<String> summaries = new List<String>();
            List<String> storypoints = new List<String>();
            List<String> ranks = new List<String>();


            int all_issues_count = all_issues.Count;
            int all_issues_sprint = 0;
            int all_issues_now = 0;
            foreach (Issue issue in all_issues)

            {//Sprint -- 10007

                if (issue.CustomFields["Sprint"].Values.Count() > 0)

                {
                    string sprint_find = "";
                    switch (project)
                    {
                        case "TARGARYENS":  sprint_find += project.Replace("TARGARYENS", "Targaryen Sprint "); break;
                        case "STARKS": sprint_find += project.Replace("STARKS", "STARKS Sprint "); break;
                        case "TA": sprint_find += project.Replace("TA", "Avengers Sprint "); break;
                        case "TTDE": sprint_find += project.Replace("TTDE", "Tropa Sprint "); break;
                        case "TD": sprint_find += project.Replace("TD", "Team DevOps Sprint "); break;
                        case "BOPE": sprint_find += project.Replace("BOPE", ""); break;
                        default: break;
                    }
                    //string sprint_find = project.Replace("TARGARYENS", "Targaryen Sprint ").Replace("STARKS", "STARKS Sprint ");
                    //sprint_find = sprint_find.Replace("TA", "Avengers Sprint ").Replace("TTDE", "Tropa Sprint ").Replace("TD", "Team DevOps Sprint ").Replace("BOPE", "");
                    if (issue.CustomFields["Sprint"].Values.Contains(sprint_find+ sprint))

                    {

                        keys.Add(issue.Key.Value.Replace("TARGARYENS", "T").Replace("STARKS", "S").Replace("TA", "A").Replace("TTDE", "TE").Replace("BOPE", "B").Replace("TD", "D"));
                    
                        
                        if (issue.Labels.Count() > 0)
                            labels.Add(issue.Labels[0]);
                        else
                            labels.Add("");

                        if (issue.CustomFields["Story Points"] != null && issue.CustomFields["Story Points"].Values != null && issue.CustomFields["Story Points"].Values.Count()>0)
                            storypoints.Add(issue.CustomFields["Story Points"].Values[0]);
                        else
                            storypoints.Add("");
                        if (issue.Summary.Contains("[") && issue.Summary.Contains("]"))
                        {
                            String[] auxiliar = issue.Summary.Split(']');
                            summaries.Add(auxiliar[1].Substring(1));
                        }
                        else
                        {
                            summaries.Add(issue.Summary);
                        }
                        
                        ranks.Add(issue.CustomFields["Rank"].Values[0]);

                    }

                }

            }
            
            List<List<String>> allLists = new List<List<String>>();



            orderLists(ref ranks,ref keys,ref summaries,ref storypoints,ref labels);
            allLists.Add(keys);
            allLists.Add(labels);
            allLists.Add(summaries);
            allLists.Add(storypoints);

            return allLists;

        }


        private List<List<String>> queryJiraIssuesInfo(List<string> projects, List<string> keysQuery, Models.Issues jiraI,string login,string password)
        {

            var jira = Jira.CreateRestClient("https://jirabancobbm.atlassian.net", login, password);
            jira.Issues.ValidateQuery = false;

            jira.Issues.MaxIssuesPerRequest = 2000;
            List<String> keys = new List<String>();
            List<String> labels = new List<String>();
            List<String> summaries = new List<String>();
            List<String> storypoints = new List<String>();
            List<String> ranks = new List<String>();

            List<Issue> all_issues = new List<Issue>();

            for (int i = 0; i < projects.Count(); i++)
            {
                string project = projects[i];
                string keyQuery = project + "-" + keysQuery[i];


                Issue issue = jira.Issues.Queryable.Where(j => j.Project == project && j.Type == "Story" && j.Key == keyQuery).FirstOrDefault();
                if (issue != null)
                {

                    keys.Add(issue.Key.Value.Replace("TARGARYENS", "T").Replace("STARKS", "S").Replace("TA", "A").Replace("TTDE", "TE").Replace("TB", "B").Replace("TD", "D"));

                    if (issue.Labels.Count() > 0)
                        labels.Add(issue.Labels[0]);
                    else
                        labels.Add("");

                    if (issue.CustomFields["Story Points"] != null && issue.CustomFields["Story Points"].Values != null && issue.CustomFields["Story Points"].Values.Count() > 0)
                        storypoints.Add(issue.CustomFields["Story Points"].Values[0]);
                    else
                        storypoints.Add("");
                    if (issue.Summary.Contains("[") && issue.Summary.Contains("]"))
                    {
                        String[] auxiliar = issue.Summary.Split(']');
                        summaries.Add(auxiliar[1].Substring(1));
                    }
                    else
                    {
                        summaries.Add(issue.Summary);
                    }

                    ranks.Add(issue.CustomFields["Rank"].Values[0]);
                }
                else
                {
                    jiraI.message += "Issue " + keyQuery + " was not found in Jira.\n";
                }
            }
            List<List<String>> allLists = new List<List<String>>();
            orderLists(ref ranks, ref keys, ref summaries, ref storypoints, ref labels);
            allLists.Add(keys);
            allLists.Add(labels);
            allLists.Add(summaries);
            allLists.Add(storypoints);
            return allLists;
        }

        [HttpPost]
        // GET: Jira
        public ActionResult getJiraIssues(string project,string sprint,string login,string password)
        {
            
            Models.Issues jiraI = new Models.Issues();

            try
            {
                var jira = Jira.CreateRestClient("https://jirabancobbm.atlassian.net", login, password);
                List<Issue> a = jira.Issues.Queryable.ToList();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unauthorized") || ex.InnerException.Message.Contains("Unauthorized"))
                {
                    jiraI.message += "Your jira credentials do not match.\n";
                }
                else
                {
                    jiraI.message += "There was some unknown error. Please try again later.\n";
                }
                return View(jiraI);
            }
            if (project != null && sprint != null)
            {
                if (project != "" && sprint != "")
                {
                    List<List<String>> allLists = getJiraIssuesInfo(project, sprint,login,password);
                    jiraI.keys = allLists[0];
                    jiraI.labels = allLists[1];
                    jiraI.summaries = allLists[2];
                    jiraI.storypoints = allLists[3];
                    if(jiraI.keys.Count==0)
                    {
                        jiraI.message += "No issues found for this sprint in this project.\n";
                    }
                }
                else
                {
                    jiraI.message += "Either the project or the sprint was left in blank.\n";
                }
            }
            else
            {
                jiraI.message += "Null parameters passed.";
            }
            return View(jiraI);
        } 

        public ActionResult queryJiraIssues(List<string> project, List<string> sprint,string login, string password)
        {
            Models.Issues jiraI = new Models.Issues();
            jiraI.message = "";
            try
            {
                var jira = Jira.CreateRestClient("https://jirabancobbm.atlassian.net", login, password);
                List<Issue> a = jira.Issues.Queryable.ToList();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Unauthorized") || ex.InnerException.Message.Contains("Unauthorized"))
                {
                    jiraI.message += "Your jira credentials do not match.\n";
                }
                else
                {
                    jiraI.message += "There was some unknown error. Please try again later.\n";
                }
                return View(jiraI);
            }
            if (project != null && sprint != null)
            {
                for (int i = 0; i < project.Count; i++)
                {
                    if (project[i] == "" || sprint[i] == "")
                    {
                        project.RemoveAt(i);
                        sprint.RemoveAt(i);
                        jiraI.message += "Issue at position " + (i + 1).ToString() + " left in blank.\n";
                    }
                }
                if (project.Count > 0)
                {
                    List<List<String>> allLists = queryJiraIssuesInfo(project, sprint, jiraI,login,password);
                    jiraI.keys = allLists[0];
                    jiraI.labels = allLists[1];
                    jiraI.summaries = allLists[2];
                    jiraI.storypoints = allLists[3];
                }
            }
            else
            {
                jiraI.message+= "Null parameters passed.";
            }
            return View(jiraI);
        }
    }
}