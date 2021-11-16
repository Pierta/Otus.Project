# Otus.Project
📚 Homework(s) in scope of a course ["Microservice Architecture"](https://otus.ru/lessons/microservice-architecture/)

---

Prerequisite for hw #1/#2:
```console
# if you don't have ingress-nginx installed, uncomment the line below and run in a console:
#kubectl apply -f k8s-manifests/common/ingress-nginx.yaml

# if a bitnami repo is not added yet, uncomment the line below and run in a console:
#helm repo add bitnami https://charts.bitnami.com/bitnami
```

How to run hw #1:
```console
# install an application with '/health' endpoint
kubectl apply -f k8s-manifests/hw-1/main-api.yaml
```

How to test hw #1:
```console
curl http://arch.homework/health # returns "Healthy"
curl http://arch.homework/home/hello # returns "hello"
curl http://arch.homework/otusapp/{student_name}/home/hello # will be forwarded to http://arch.homework/home/hello
# remove all the resources
kubectl delete -f k8s-manifests/hw-1/main-api.yaml
```

---

How to run hw #2:
```console
# install postgres instance with metrics exporter
helm install db bitnami/postgresql -f postgres-chart/values.yaml --namespace otus-project --create-namespace --wait
# install an application with crud api for managing users
helm install crud-api crud-api-chart/ --namespace otus-project
```

How to test hw #2:
```console
curl http://arch.homework/liveness # returns "Healthy"
curl http://arch.homework/readiness # returns "Healthy"
curl http://arch.homework/users # returns list of 3 users
# remove all the resources
#kubectl delete namespace otus-project
```
Also, you can import [a postman collection](https://github.com/Pierta/Otus.Project/blob/develop/k8s-manifests/hw-2/Otus.Project.postman_collection.json)
