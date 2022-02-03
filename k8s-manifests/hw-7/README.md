# Otus.Project
📚 Homework #7

---

Prerequisites:
```console
# if a bitnami repo is not added yet, uncomment the line below and run in a console
#helm repo add bitnami https://charts.bitnami.com/bitnami
# update newly added helm repos
#helm repo update

# if you don't have newman installed, uncomment the line below and run in a console
#npm install -g newman
```

---

hw #7 schema:

Used hw-6 schema (http communication, but message broker only for notifications):
![schema](../hw-6/schema.png)

How to run hw #7:
```console
# https://kubernetes.github.io/ingress-nginx/deploy/#quick-start
# if you don't have ingress-nginx installed, uncomment the line below and run in a console
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml

# use helm charts from hw-6
cd ../hw-6
# install postgres instance
helm install db bitnami/postgresql -f postgres-chart/values.yaml --namespace otus-project --create-namespace --atomic
# install an application with crud api for managing users
helm install crud-api crud-api-chart/ --namespace otus-project --atomic
# install auth api
helm install auth-api auth-api-chart/ --namespace otus-project --atomic
# install billing api
helm install billing-api billing-api-chart/ --namespace otus-project --atomic
# install rabbitmq instance
helm install sbus bitnami/rabbitmq -f rabbitmq-chart/values.yaml --namespace otus-project --atomic
# install notification api
helm install notification-api notification-api-chart/ --namespace otus-project --atomic
# install order api
helm install order-api order-api-chart/ --namespace otus-project --atomic
```

How to test hw #7:
```console
cd ../hw-7
newman run postman_collection.json

# remove all the resources
kubectl delete namespace otus-project

# delete ingress-nginx after testing is done, uncomment the line below and run in a console
#kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml
```
