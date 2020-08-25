#!/bin/bash 
http_response=$(curl -s -o response.txt -w "%{http_code}" https://dev.azure.com/marielletnolasco/Milestone4Solution/_apis/build/status/5?api-version=6.0-preview.1)
if [ $http_response = "200" ] 
then
    echo "Server responsive"
    echo "Build Status:"
    scount=$(grep -c 'succeeded' response.txt)
    if [ $scount -gt 0 ] 
    then 
        echo "Success!" 
        echo "Checking yaml file"
        build=$(grep -c 'build' azure-pipelines.yml)
        if [ $build -gt 0 ]
        then 
            #check if yml contains build command check if contains other commands
            echo "pipeline has build command"
        else
            echo "build command not found, pipeline flawed"
        fi  
    else
        echo "Failed!"
    fi
else
echo "Server unresponsive"
fi
