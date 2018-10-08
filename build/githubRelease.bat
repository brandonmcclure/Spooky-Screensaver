create_release_url="https://api.github.com/repos/brandonmcclure/Spooky-Screensaver/releases"

response=$(curl --request POST -H "Content-Type: application/json" -H "Authorization: token $GITHUB_PAT" --data "$json" $create_release_url)

github_release_id=$(echo $response | jq -r ".id");

assets_url_template=$(echo $response | jq -r ".upload_url"); github_assets_url=${assets_url_template%"{?name,label}"};

drops="$SYSTEM_ARTIFACTSDIRECTORY/SpiderScreensaver/bin/Release";

for filename in $drop_folder/*.zip
do
  curl                    
    --request POST        
    --data-binary "@$filename" -H "Authorization: token $GITHUB_PAT"
    -H "Content-Type: application/octet-stream"
    $github_assets_url?name=$(basename $filename);
done

json='{"tag_name": "Oct-18", "target_commitish": "master", "name": "Oct-18", "body": "Build as of Sep-18", "draft": false, "prerelease": false}'

edit_release_url="https://api.github.com/repos/headmelted/prebootstrap/releases/$github_release_id"

curl --request PATCH -H "Authorization: token $GITHUB_PAT" -H "Content-Type: application/json" --data "$json" "$edit_release_url";