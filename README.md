# BusinessAdvisors
Application that allows you to manage the control of sales of commercial advisors Softtek Perú

### Docker commands used 
- `docker build --rm -t productive-dev/cloud-assesors:latest .`

- `docker image ls`

- `docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 productive-dev/cloud-assesors`

- `docker ps`

- `docker container stop [ID-DOCKER]`

### Docker container
![Softtek1](https://github.com/zZ30MaxZz/BusinessAdvisors/blob/master/images/DockerDeploy.png?raw=true 'Softtek Test')

### App Consuming Docker 
![Softtek1](https://github.com/zZ30MaxZz/BusinessAdvisors/blob/master/images/DockerDeploy2.png?raw=true 'Softtek Test')