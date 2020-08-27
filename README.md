# Milestone4Solution
M4 working bashscript for checking pipelines
<br>
To get the build status of a pipeline in Azure follow the format: https://dev.azure.com/{organization}/{project}/_apis/build/status/{definition}?api-version=6.0-preview.1
Documentation can be found here: https://docs.microsoft.com/en-us/rest/api/azure/devops/build/status/get?view=azure-devops-rest-6.0

## Files
### test.sh
This is the draft bash script for testing if the pipeline builds. Includes logic that checks pipeline yml file to see if it actually has the necessary commands. For now, it's just build.

### azure-pipelines.yml
Test yml file to check if the the bash script works. Will need to be removed for actual batch, the bash script would need to change the pipeline file the bash script is testing. 

### creds.txt
This is where you put in your pipeline credentials. First line must be the organization, second line the project, and the third line will be the definition id of your pipeline.
