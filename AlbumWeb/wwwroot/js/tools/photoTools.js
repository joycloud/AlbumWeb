angular.module('App', [])

.controller('ImageLayout', ImageLayout)


function ImageLayout($scope, $http){
  $http.get('https://xieranmaya.github.io/images/cats/cats.json').success(function(imgs){
    $scope.imgs = imgs
  })
}