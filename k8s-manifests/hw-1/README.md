# Otus.Project
📚 Homework #1

---

How to run hw #1:
```console
# https://kubernetes.github.io/ingress-nginx/deploy/#quick-start
# if you don't have ingress-nginx installed, uncomment the line below and run in a console
#kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml

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

# delete ingress-nginx after testing is done, uncomment the line below and run in a console
#kubectl delete -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.5/deploy/static/provider/cloud/deploy.yaml
```
