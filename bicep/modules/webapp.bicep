param webAppName string
param serviceplanName string
param location string
param sku string

resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name:serviceplanName
  location:location
  sku:{
    name: sku
  }
}

resource webApps 'Microsoft.Web/sites@2021-02-01' = {
  name: webAppName
  location: location
  properties:{
    serverFarmId:appServicePlan.id
  }
}
