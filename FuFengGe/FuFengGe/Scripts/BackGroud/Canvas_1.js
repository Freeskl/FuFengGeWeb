var can = document.getElementById("backCanvas"); //获取canvas对象
var ctx = can.getContext("2d");  //设置绘图环境
//获取浏览器宽高
var w = can.width = window.innerWidth;
var h = can.height = window.innerHeight;
//不断调整画布，使其随浏览器变化而变化
window.onresize = function () {
    w = can.width = window.innerWidth;
    h = can.height = window.innerHeight;
}

//设置画笔颜色
//ctx.fillStyle = "#f0ffff";
ctx.fillStyle = "#ffffff";

//鼠标的定义
var pointer = {
    x: null,
    y: null,
    max: 20000
};

//鼠标屏幕内移动
window.onmousemove = function (e) {
    e = e || window.event;
    pointer.x = e.clientX;
    pointer.y = e.clientY;
}

//鼠标移出屏幕
window.mouseout = function () {
    pointer.x = null;
    pointer.y = null;
}

var dots = []; //点的数组

//注册点
for (i = 0; i < 100; i++) {
    var x = Math.random() * w;
    var y = Math.random() * h;
    var xa = Math.random() * 2-1;
    var ya = Math.random() * 2-1;
    dots.push({
        x: x,
        y: y,
        xa: xa,
        ya: ya,
        max: 6000
    });
}

//动画效果

var RAF = (function () {
    return window.requestAnimationFrame || window.webkitRequestAnimationFrame || window.mozRequestAnimationFrame || window.oRequestAnimationFrame || window.msRequestAnimationFrame || function (callback) {
        window.setTimeout(callback, 1000 / 60);
    };
})();

//主体程序
setTimeout(animate, 100);

//单帧画面绘制
function animate() {
    ctx.clearRect(0, 0, can.width, can.height);

    //对所有生成的点进行绘制
    dots.forEach(function (item) {
        item.x += item.xa;
        item.y += item.ya;

        //正反向判断
        item.xa *= (item.x > w || item.x < 0 ? -1 : 1);
        item.ya *= (item.y > h || item.x < 0 ? -1 : 1);

        //绘制点
        ctx.fillRect(item.x - 0.5, item.y - 0.5, 1, 1);

        //加上鼠标重新计算点间的距离
        temdots = [pointer].concat(dots);
        for (var j = 0; j < temdots.length; j++) {
            var temdot = temdots[j];
            if (temdot.x === null || temdot.y === null || temdot === item) continue;

            var xl = item.x - temdot.x;
            var yl = item.y - temdot.y;

            //距离计算
            var dis = xl * xl + yl * yl;

            // 如果两个粒子之间的距离小于粒子对象的max值，则在两个粒子间画线
            if (dis < 20000) {
                // 如果是鼠标，则让粒子向鼠标的位置移动，
                if (temdot === pointer && dis > 3000) {
                    item.x -= xl * 0.03;
                    item.y -= yl * 0.03;
                }

                var radio=(20000-dis)/20000

                // 画线
                ctx.beginPath();
                ctx.lineWidth = 0.2;
                ctx.strokeStyle = 'rgba(0,0,0,' + (radio + 0.2) + ')';
                //ctx.strokeStyle = "#f0ffff";
                ctx.moveTo(temdot.x, temdot.y);
                ctx.lineTo(item.x, item.y);
                ctx.stroke();
                // 将已经计算过的粒子从数组中删除
                temdots.splice(temdots.indexOf(temdot), 1);
            }

        }
    });
    RAF(animate);
}






