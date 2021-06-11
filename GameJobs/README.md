docker run -d --name ft.g8.game-jobs \
    -e "TZ=Asia/Ho_Chi_Minh" \
    -e "ASPNETCORE_ENVIRONMENT=Development" \
    asia.gcr.io/flextech-production/ft.g8.game-jobs:dev.20201226143427
    
docker run -d --name ft.g8.game-jobs \
    -e "TZ=Asia/Ho_Chi_Minh" \
    asia.gcr.io/flextech-production/ft.g8.game-jobs:latest    