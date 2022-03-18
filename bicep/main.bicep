param location string = resourceGroup().location
param appName string ='azdevops'
param appSku string ='F1'

module WebApp 'modules/webapp.bicep' =  {
  name: appName
  params: {
    serviceplanName : 'SP-${appName}'
    location: location
    sku:appSku
    webAppName: appName
  }
}
