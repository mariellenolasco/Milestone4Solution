#!/bin/bash 
organization=$(sed '1q;d' creds.txt)
project=$(sed '2q;d' creds.txt)
definition=$(sed '3q;d' creds.txt)
http_response=$(curl -s -o response.txt -w "%{http_code}" https://dev.azure.com/${organization}/${project}/_apis/build/status/${definition}?api-version=6.0-preview.1)
if [ $http_response = "200" ] 
then
    echo "Server responsive"
    echo "Build Status:"
    scount=$(grep -c 'succeeded' response.txt)
    if [ $scount -gt 0 ] 
    then      
        missing=0
        echo "Success!" 
        echo "Checking yaml file"
        echo "Checking for build command"
        build=$(grep -c 'dotnet build' azure-pipelines.yml)
        if [ $build -gt 0 ]
        then 
            #check if yml contains build command check if contains other commands
            echo "Pipeline has build command"
        else
            echo "build command not found, pipeline flawed"
            $missing++
        fi
        echo "Checking for testing"
        test=$(grep -c 'dotnet test' azure-pipelines.yml)
        if [ $test -gt 0]
        then    
            echo "Pipeline has testing"
        else
            echo "test command not found, pipeline flawed"
            $missing++
        fi
        echo "Checking if sonar cloud is set up"
        sonarc=$(grep -c 'SonarCloudPrepare@1' azure-pipelines.yml)
        if [ $sonarc -gt 0]
        then    
            echo "Pipeline has sonar cloud set up"
        else
            echo "sonar cloud set up not found, pipeline flawed"
            $missing++
        fi
        echo "Checking if code analysis was run"
        codeanal=$(grep -c 'SonarCloudAnalyze@1' azure-pipelines.yml)
        if [ $codeanal -gt 0]
        then    
            echo "Pipeline runs code analysis"
        else
            echo "code analysis run task not found, pipeline flawed"
            $missing++
        fi
        echo "Checking if code analysis was published"
        codeanalpub=$(grep -c 'SonarCloudPublish@1' azure-pipelines.yml)
        if [ $codeanal -gt 0]
        then    
            echo "Pipeline publishes code analysis"
        else
            echo "code analysis publish task not found, pipeline flawed"
            $missing++
        fi 
        echo "Checking if webapp was deployed"
        deployed=$(grep -c 'AzureRmWebAppDeployment@4' azure-pipelines.yml)
        if [ $deployed -gt 0]
        then    
            echo "Pipeline deploys webapp"
        else
            echo "webapp wasn't deployed"
            $missing++
        fi
        if [$missing -gt 0]
        then
            echo "More than one essential task missing from pipeline, Fail!"
        else    
            echo "Passed!"
        fi
    else
        echo "Failed!"
    fi

else
echo "Server unresponsive. Please check if your credentials are valid"
fi
