# Otus.Project
📚 Homework(s) in scope of a course ["Microservice Architecture"](https://otus.ru/lessons/microservice-architecture/)

How to run hw #1:
```console
kubectl apply -f k8s-manifests/ingress-nginx.yaml
kubectl apply -f k8s-manifests/simple-api.yaml
```

How to test hw #1:
```console
curl http://arch.homework/health # returns "Healthy"
curl http://arch.homework/home/hello # returns "hello"
curl http://arch.homework/otusapp/{student_name}/home/hello # will be forwarded to http://arch.homework/home/hello
```