# Otus.Project
📚 Homework #2

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

How to run hw #2:
```console
# https://kubernetes.github.io/ingress-nginx/deploy/#quick-start
# if you don't have ingress-nginx installed, uncomment the line below and run in a console
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml

cd k8s-manifests/hw-2/
# install postgres instance with metrics exporter
helm install db bitnami/postgresql -f postgres-chart/values.yaml --namespace otus-project --create-namespace --atomic
# install an application with crud api for managing users
helm install crud-api crud-api-chart/ --namespace otus-project --atomic
```

How to test hw #2:
```console
curl http://arch.homework/liveness # returns "Healthy"
curl http://arch.homework/readiness # returns "Healthy"
curl http://arch.homework/users # returns list of 3 users

# alternative approach for testing
newman run postman_collection.json

# remove all the resources
kubectl delete namespace otus-project

# delete ingress-nginx after testing is done, uncomment the line below and run in a console
#kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml
```
