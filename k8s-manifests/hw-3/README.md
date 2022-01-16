# Otus.Project
📚 Homework #3

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

How to run hw #3:
```console
cd k8s-manifests/hw-3/
# install prometheus stack
helm install prometheus prometheus-community/kube-prometheus-stack -f prometheus-chart/values.yaml --namespace otus-project --create-namespace --atomic
# install ingress-nginx with enabled service monitor
helm install nginx ingress-nginx/ingress-nginx -f ingress-nginx-chart/values.yaml --namespace otus-project --atomic

# import the following dashboard to grafana via configmap
# k8s-manifests/hw-3/prometheus-chart/grafana_dashboard.json
kubectl apply -f prometheus-chart/grafana_configmap.yaml --namespace otus-project

cd ../hw-2/
# install postgres instance with metrics exporter
helm install db bitnami/postgresql -f postgres-chart/values.yaml --namespace otus-project --atomic
# install an application with crud api for managing users
helm install crud-api crud-api-chart/ --namespace otus-project --atomic

# run prometheus on localhost:9090
kubectl port-forward service/prometheus-kube-prometheus-prometheus -n otus-project 9090
# run grafana on localhost:9000 (admin:prom-operator)
kubectl port-forward service/prometheus-grafana -n otus-project 9000:80
```

How to test hw #3:
```console
# run a simple load test
ab -n 5000 -c 5 http://arch.homework/users

# remove all the resources
kubectl delete namespace otus-project
kubectl delete crd alertmanagerconfigs.monitoring.coreos.com
kubectl delete crd alertmanagers.monitoring.coreos.com
kubectl delete crd podmonitors.monitoring.coreos.com
kubectl delete crd probes.monitoring.coreos.com
kubectl delete crd prometheuses.monitoring.coreos.com
kubectl delete crd prometheusrules.monitoring.coreos.com
kubectl delete crd servicemonitors.monitoring.coreos.com
kubectl delete crd thanosrulers.monitoring.coreos.com
```
