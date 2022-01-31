# Otus.Project
📚 Homework #5

---

Prerequisites:
```console
# if a bitnami repo is not added yet, uncomment the line below and run in a console
#helm repo add bitnami https://charts.bitnami.com/bitnami
# if a prometheus-community repo is not added yet, uncomment the line below and run in a console
#helm repo add prometheus-community https://prometheus-community.github.io/helm-charts
# if a ingress-nginx repo is not added yet, uncomment the line below and run in a console
#helm repo add ingress-nginx https://kubernetes.github.io/ingress-nginx
# update newly added helm repos
#helm repo update

# if you don't have newman installed, uncomment the line below and run in a console
#npm install -g newman
```

---

hw #5 schema:

![schema](../../k8s-manifests/hw-5/schema.png)

How to run hw #5:
```console
# https://kubernetes.github.io/ingress-nginx/deploy/#quick-start
# if you don't have ingress-nginx installed, uncomment the line below and run in a console
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml

# install postgres instance
helm install db bitnami/postgresql -f postgres-chart/values.yaml --namespace otus-project --create-namespace --atomic
# install an application with crud api for managing users
helm install crud-api crud-api-chart/ --namespace otus-project --atomic
# install auth api
helm install auth-api auth-api-chart/ --namespace otus-project --atomic
```

How to test hw #5:
```console
newman run postman_collection.json

# remove all the resources
kubectl delete namespace otus-project

# delete ingress-nginx after testing is done, uncomment the line below and run in a console
#kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml
```
