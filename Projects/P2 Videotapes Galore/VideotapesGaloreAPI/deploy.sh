ssh -i ./videotapes-galore-key.pem ec2-user@35.176.20.49 << EOF
    cd VideotapesGaloreAPI/VideotapesGaloreAPI
    git pull
    cd VideotapesGalore.WebApi
    sudo fuser -k 1337/tcp
    sudo rm -rf ./publish

    sudo dotnet publish -o ./publish
    cd publish/
    sudo nohup dotnet VideotapesGalore.WebApi.dll --urls "http://*:1337" > ~/log.txt 2>&1 &
EOF
