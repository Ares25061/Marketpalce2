FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-end

WORKDIR /app

COPY . ./   
RUN dotnet publish ./BlazorClient.csproj -c Release -o output

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build-end /app/output/wwwroot .

COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]