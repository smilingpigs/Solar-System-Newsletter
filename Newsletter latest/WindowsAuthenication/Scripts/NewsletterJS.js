
        $(document).ready(function() {
            var $startAnim = $("#start-anim");
            var $demoText = $("#demo-text");

            //Split the text into characters and wrap every character into span element, then convert the whitespaces to whitespace characters.
            $demoText.html($demoText.html().replace(/./g, "<span>$&</span>").replace(/\s/g, "&nbsp;"));

            // $startAnim.on("click", startAnimation);
            startAnimation();

            function startAnimation() {
                TweenLite.set($startAnim, {
                    autoAlpha: 0
                });

                //TweenMax.staggerFromTo( $demoText.find("span"), 0.4, {autoAlpha:0, scale:0}, {autoAlpha:1, scale:1}, 0.1, reset );

                TweenMax.staggerFromTo($demoText.find("span"), 0.4, {
                    autoAlpha: 0,
                    scale: 7
                }, {
                    autoAlpha: 1,
                    scale: 1
                }, 0.1, reset);

            }

            function reset() {
                TweenMax.to($startAnim, 1, {
                    autoAlpha: 1
                });
            }
        });

    $(document).ready(function(){
        $("a#icon2").click(function(){
            $("div#p2").addClass('left');
            $('div:not(#p2)').removeClass('left');
            var $demoText = $("#content");
            var pic = $("#picture");
            TweenMax.from($demoText, 1, {x:-300, opacity:0, scale:0.5});
            TweenMax.from(pic, 2, { x: -300, opacity: 0, scale: 0.5 });
            slide1();
        });
        $("a#icon3").click(function(){
            $("div#p3").addClass('left');
            $('div:not(#p3)').removeClass('left');
            slide2();
        });
        $("a#icon4").click(function(){
            $("div#p4").addClass('left');
            $('div:not(#p4)').removeClass('left');
            slide3();
        });
        $("a#icon5").click(function(){
            $("div#p5").addClass('left');
            $('div:not(#p5)').removeClass('left');
            slide4();
        });
        $("a#icon6").click(function(){
            $("div#p6").addClass('left');
            $('div:not(#p6)').removeClass('left');
            slide5();
        });
        $("a#icon7").click(function(){
            $("div#p7").addClass('left');
            $('div:not(#p7)').removeClass('left');
            slide6();
        });
        $("a#icon8").click(function(){
            $("div#p8").addClass('left');
            $('div:not(#p8)').removeClass('left');
            slide7();
        });
        $("a#icon").click(function(){
            $('div').removeClass('left');
        });
    });

    function testMe() {
        var logo = $("#container");var logo1 = $("#container1");
        TweenMax.staggerTo(logo, 1,
                       {scale:0.8, opacity:1}, 0.25);
        TweenMax.staggerTo(logo1, 1,
                      {scale:0, opacity:0.3}, 0.25);
        var username = $(".effect-2").val();
        username = username.toLowerCase().replace(/\b[a-z]/g, function(letter) {
            return letter.toUpperCase();
        });
        $("span.userName").html("Hi "+username);
        var test = $("#test");
        TweenLite.from(test, 2, {opacity:0, right:"300px"});
    }

    var card = $(".content");
$(document).on("mousemove", function(e) {
    var ax = -($(window).innerWidth() / 2 - e.pageX) / 20;
    var ay = ($(window).innerHeight() / 2 - e.pageY) / 10;
    card.attr("style", "transform: rotateY(" + ax + "deg) rotateX(" + ay + "deg);-webkit-transform: rotateY(" + ax + "deg) rotateX(" + ay + "deg);-moz-transform: rotateY(" + ax + "deg) rotateX(" + ay + "deg)");
});



function slide1() {
//1 let scene, camera, renderer, cube, sphere, torus, planet, shape;
let scene, camera, renderer, light1, light2, rayCast, mouse;
let b, sphereTab = [];
let ADD = 0.003,
  theta = 0;
let ADD1 = 0.0003;

let createSphere = function () {
    let geometry = new THREE.SphereGeometry(5, 30, 30);
    var texture = new THREE.TextureLoader().load('../Content/textures/earthSurface.jpg');
    let material = new THREE.MeshPhongMaterial({
        shininess: 0,
        map: texture
    });
    b = new THREE.Mesh(geometry, material);
    scene.add(b);
}

let init = function () {
    scene = new THREE.Scene();
    scene.background = new THREE.Color(0x000000);

    camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
    //camera.position.z = 30;
    camera.position.set(-11, -4, 15);

    light1 = new THREE.PointLight(0xffffff, 2, 50);
    light1.position.set(5, 5, 15);
    scene.add(light1);



    rayCast = new THREE.Raycaster();
    mouse = new THREE.Vector2();
    mouse.x = mouse.y = -1;

    var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
    for (var i = 0; i < 250; i++) {
        // randRadius = Math.random()*30+10;
        lumiereS = new THREE.MeshPhongMaterial({
            emissive: '#fff'
        });
        sphereTab.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
    }
    var posX = -3000;
    var posY = -3000;
    for (var i = 0; i < sphereTab.length; i++) {
        sphereTab[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
        // sphereTab[i].material.color.set(0x7cfc00);
        scene.add(sphereTab[i]);
    }

    createSphere();

    var container = document.getElementById('p2');
    document.body.appendChild(container);
    renderer = new THREE.WebGLRenderer();
    renderer.setSize(window.innerWidth, window.innerHeight);
    container.appendChild(renderer.domElement);
};

// main animation loop - calls 50-60 times per second.
let mainLoop = function () {
    var timer = 0.00001 * Date.now();
    for (var i = 0, il = sphereTab.length; i < il; i++) {
        var sfere = sphereTab[i];
        sfere.position.x = 800 * Math.sin(timer + i);
        // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
        sfere.position.z = 800 * Math.sin(timer + i * 1.1);
    }

    // camera.lookAt(b.position);
    // camera.rotation.y += 0.2 * Math.PI / 180;
    // b.position.z = 25 * Math.cos(theta);
    b.rotation.y += ADD;

    theta += ADD1;

    renderer.render(scene, camera);
    test = requestAnimationFrame(mainLoop);
};

init();
mainLoop();
}


function slide2() {
    //2 let scene, camera, renderer, cube, sphere, torus, planet, shape;
    let scene1, camera1, renderer1;
    let b1, sphereTab1 = [];
    let ADDJ1 = 0.05,
        theta1 = 0;
    let ADDJ11 = 0.002;

    let createJupiter = function () {
        let geometry = new THREE.SphereGeometry(3, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/jupitermap.jpg');
        let material = new THREE.MeshPhongMaterial({
            shininess: 0,
            map: texture
        });
        b1 = new THREE.Mesh(geometry, material);
        scene1.add(b1);
    }

    let init1 = function () {
        scene1 = new THREE.Scene();
        scene1.background = new THREE.Color(0x000000);
        camera1 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera1.position.set(-8, -3, 15);
        // light1 = new THREE.PointLight(0xffffff, 2, 50);
        // light1.position.set(5, 5, 15);
        // scene.add(light1);

        const light = new THREE.DirectionalLight(0xffffff);
        light.position.set(1, 0, 1).normalize();
        scene1.add(light);

        rayCast = new THREE.Raycaster();
        mouse = new THREE.Vector2();
        mouse.x = mouse.y = -1;

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#ffd700'
            });
            sphereTab1.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab1.length; i++) {
            sphereTab1[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene1.add(sphereTab1[i]);
        }

        createJupiter();

        var container = document.getElementById('p3');
        console.log('tewdfsdf');
        console.log(container);
        document.body.appendChild(container);
        renderer1 = new THREE.WebGLRenderer();
        renderer1.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer1.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop1 = function () {
        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab1.length; i < il; i++) {
            var sfere = sphereTab1[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        // camera1.lookAt(new THREE.Vector3(-8,-3,10));
        // camera1.rotation.x = 1 * Math.sin(theta1);
        // camera1.position.z = 10 * Math.cos(theta1);
        b1.rotation.z += ADDJ11;
        camera1.fov += ADDJ1;
        camera1.updateProjectionMatrix();
        if (camera1.fov > 100 || camera1.fov < 50)
            ADDJ1 *= -1;

        // * Math.PI / 180;
        // b1.position.z = 25 * Math.cos(theta);
        // b1.rotation.y += ADDJ1;

        // theta += ADDJ11;

        renderer1.render(scene1, camera1);
        requestAnimationFrame(mainLoop1);
    };

    init1();
    mainLoop1();
}


function slide3() {


    let scene2, camera2, renderer2,light1;
    let planet, sphereTab2 = [];
    let rings = [];
    let ADDS11 = 0.007;
    let ADDS1 = 0.02;
    
    let createSaturn = function () {
        let geometry = new THREE.SphereGeometry(4, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/saturnSurface.jpg');
        let material = new THREE.MeshPhongMaterial({ map: texture });

        planet = new THREE.Mesh(geometry, material);
        scene2.add(planet);
        
        texture = new THREE.TextureLoader().load('../Content/textures/jupitermap.jpg');
     
        geometry = new THREE.TorusGeometry(6.7, 0.7, 2, 1000);
        material = new THREE.MeshLambertMaterial({ map: texture });

        let ring = new THREE.Mesh(geometry, material);
        rings.push(ring);

        rings.forEach(ring => {
            ring.rotation.x = 1.8;
            ring.rotation.y = 0.5;
            scene2.add(ring);
        });
        //let geometry = new THREE.SphereGeometry(3, 30, 30);
        //var texture = new THREE.TextureLoader().load('../Content/textures/jupitermap.jpg');
        //let material = new THREE.MeshPhongMaterial({
        //    shininess: 0,
        //    map: texture
        //});
        //planet = new THREE.Mesh(geometry, material);
        //scene2.add(planet);
    };

    let init2 = function () {
        scene2 = new THREE.Scene();
        scene2.background = new THREE.Color(0x000000);
        camera2 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera2.position.set(-8, -3, 10);
        light1 = new THREE.PointLight(0xffffff, 2, 50);
        light1.position.set(2, 2, 25);
        scene2.add(light1);

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#fff'
            });
            sphereTab2.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab2.length; i++) {
            sphereTab2[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene2.add(sphereTab2[i]);
        }

        createSaturn();

        var container = document.getElementById('p4');
        document.body.appendChild(container);
        renderer2 = new THREE.WebGLRenderer();
        renderer2.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer2.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop2 = function () {
        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab2.length; i < il; i++) {
            var sfere = sphereTab2[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        planet.rotation.y += ADDS11;
        rings[0].rotation.z += ADDS1;

        renderer2.render(scene2, camera2);
        requestAnimationFrame(mainLoop2);
    };

    init2();
    mainLoop2();
}

function slide4() {
    //4 let scene, camera, renderer, cube, sphere, torus, planet, shape;
    let scene3, camera3, renderer3;
    let b3, sphereTab3 = [];
    let ADD3 = 0.003;
    let ADD13 = 0.0003;

    let createSphere3 = function () {
        let geometry = new THREE.SphereGeometry(5, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/mercurySurface.jpg');
        let material = new THREE.MeshPhongMaterial({
            shininess: 0,
            map: texture
        });
        b3 = new THREE.Mesh(geometry, material);
        scene3.add(b3);
    }

    let init3 = function () {
        scene3 = new THREE.Scene();
        scene3.background = new THREE.Color(0x000000);

        camera3 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera3.position.set(-11, -4, 15);

        light1 = new THREE.PointLight(0xffffff, 2, 50);
        light1.position.set(5, 5, 15);
        scene3.add(light1);

        // const light = new THREE.DirectionalLight( 0xffffff );
        // light.position.set( 1, 0, 1 ).normalize();
        // scene.add(light);

        rayCast = new THREE.Raycaster();
        mouse = new THREE.Vector2();
        mouse.x = mouse.y = -1;

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#fff'
            });
            sphereTab3.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab3.length; i++) {
            sphereTab3[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene3.add(sphereTab3[i]);
        }

        createSphere3();

        var container = document.getElementById('p5');
        document.body.appendChild(container);
        renderer3 = new THREE.WebGLRenderer();
        renderer3.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer3.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop3 = function () {

        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab3.length; i < il; i++) {
            var sfere = sphereTab3[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        // camera.lookAt(b.position);
        // camera.rotation.y += 0.2 * Math.PI / 180;
        // b.position.z = 25 * Math.cos(theta);
        b3.rotation.y += ADD3;



        renderer3.render(scene3, camera3);
        requestAnimationFrame(mainLoop3);
    };

    init3();
    mainLoop3();
}

function slide5() {
    //5 let scene, camera, renderer, cube, sphere, torus, planet, shape;
    let scene4, camera4, renderer4;
    let b4, sphereTab4 = [];
    let ADD4 = 0.003;
    let ADD14 = 0.0003;

    let createSphere4 = function () {
        let geometry = new THREE.SphereGeometry(5, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/venusSurface.jpg');
        let material = new THREE.MeshPhongMaterial({
            shininess: 0,
            map: texture
        });
        b4 = new THREE.Mesh(geometry, material);
        scene4.add(b4);
    }

    let init4 = function () {
        scene4 = new THREE.Scene();
        scene4.background = new THREE.Color(0x000000);

        camera4 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera4.position.set(-11, -4, 15);

        light1 = new THREE.PointLight(0xffffff, 2, 50);
        light1.position.set(5, 5, 15);
        scene4.add(light1);

        // const light = new THREE.DirectionalLight( 0xffffff );
        // light.position.set( 1, 0, 1 ).normalize();
        // scene.add(light);

        rayCast = new THREE.Raycaster();
        mouse = new THREE.Vector2();
        mouse.x = mouse.y = -1;

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#fff'
            });
            sphereTab4.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab4.length; i++) {
            sphereTab4[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene4.add(sphereTab4[i]);
        }

        createSphere4();

        var container = document.getElementById('p6');
        console.log('tewdfsdf');
        console.log(container);
        document.body.appendChild(container);
        renderer4 = new THREE.WebGLRenderer();
        renderer4.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer4.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop4 = function () {

        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab4.length; i < il; i++) {
            var sfere = sphereTab4[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        // camera.lookAt(b.position);
        // camera.rotation.y += 0.2 * Math.PI / 180;
        // b.position.z = 25 * Math.cos(theta);
        b4.rotation.y += ADD4;



        renderer4.render(scene4, camera4);
        requestAnimationFrame(mainLoop4);
    };

    init4();
    mainLoop4();
}

function slide6() {
    //6 let scene, camera, renderer, cube, sphere, torus, planet, shape;
    let scene5, camera5, renderer5;
    let b5, sphereTab5 = [];
    let ADD5 = 0.003;
    let ADD15 = 0.0003;

    let createSphere5 = function () {
        let geometry = new THREE.SphereGeometry(5, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/marsSurface.png');
        let material = new THREE.MeshPhongMaterial({
            shininess: 0,
            map: texture
        });
        b5 = new THREE.Mesh(geometry, material);
        scene5.add(b5);
    }

    let init5 = function () {
        scene5 = new THREE.Scene();
        scene5.background = new THREE.Color(0x000000);

        camera5 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera5.position.set(-11, -4, 15);

        light1 = new THREE.PointLight(0xffffff, 2, 50);
        light1.position.set(5, 5, 15);
        scene5.add(light1);

        // const light = new THREE.DirectionalLight( 0xffffff );
        // light.position.set( 1, 0, 1 ).normalize();
        // scene.add(light);

        rayCast = new THREE.Raycaster();
        mouse = new THREE.Vector2();
        mouse.x = mouse.y = -1;

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#fff'
            });
            sphereTab5.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab5.length; i++) {
            sphereTab5[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene5.add(sphereTab5[i]);
        }

        createSphere5();

        var container = document.getElementById('p7');
        console.log('tewdfsdf');
        console.log(container);
        document.body.appendChild(container);
        renderer5 = new THREE.WebGLRenderer();
        renderer5.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer5.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop5 = function () {

        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab5.length; i < il; i++) {
            var sfere = sphereTab5[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        // camera.lookAt(b.position);
        // camera.rotation.y += 0.2 * Math.PI / 180;
        // b.position.z = 25 * Math.cos(theta);
        b5.rotation.y += ADD5;



        renderer5.render(scene5, camera5);
        requestAnimationFrame(mainLoop5);
    };

    init5();
    mainLoop5();
}

function slide7() {
    //7 let scene, camera, renderer, cube, sphere, torus, planet, shape;
    let scene6, camera6, renderer6;
    let b6, sphereTab6 = [];
    let ADD6 = 0.003;
    let ADD16 = 0.0003;

    let createSphere6 = function () {
        let geometry = new THREE.SphereGeometry(5, 30, 30);
        var texture = new THREE.TextureLoader().load('../Content/textures/plutoSurface.jpg');
        let material = new THREE.MeshPhongMaterial({
            shininess: 0,
            map: texture
        });
        b6 = new THREE.Mesh(geometry, material);
        scene6.add(b6);
    }

    let init6 = function () {
        scene6 = new THREE.Scene();
        scene6.background = new THREE.Color(0x000000);

        camera6 = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 1, 1000);
        //camera.position.z = 30;
        camera6.position.set(-11, -4, 15);

        light1 = new THREE.PointLight(0xffffff, 2, 50);
        light1.position.set(5, 5, 15);
        scene6.add(light1);

        // const light = new THREE.DirectionalLight( 0xffffff );
        // light.position.set( 1, 0, 1 ).normalize();
        // scene.add(light);

        rayCast = new THREE.Raycaster();
        mouse = new THREE.Vector2();
        mouse.x = mouse.y = -1;

        var geoSphere = new THREE.SphereGeometry(Math.random() * 1, 20, 20);
        for (var i = 0; i < 250; i++) {
            // randRadius = Math.random()*30+10;
            lumiereS = new THREE.MeshPhongMaterial({
                emissive: '#fff'
            });
            sphereTab6.push(new THREE.Mesh(new THREE.SphereGeometry(Math.random() * 1, 15, 15), lumiereS));
        }
        var posX = -3000;
        var posY = -3000;
        for (var i = 0; i < sphereTab6.length; i++) {
            sphereTab6[i].position.set(Math.random() * 600 - 300, Math.random() * 600 - 300, Math.random() * 600 - 300);
            // sphereTab[i].material.color.set(0x7cfc00);
            scene6.add(sphereTab6[i]);
        }

        createSphere6();

        var container = document.getElementById('p8');
        console.log('tewdfsdf');
        console.log(container);
        document.body.appendChild(container);
        renderer6 = new THREE.WebGLRenderer();
        renderer6.setSize(window.innerWidth, window.innerHeight);
        container.appendChild(renderer6.domElement);
    };

    // main animation loop - calls 50-60 times per second.
    let mainLoop6 = function () {

        var timer = 0.00001 * Date.now();
        for (var i = 0, il = sphereTab6.length; i < il; i++) {
            var sfere = sphereTab6[i];
            sfere.position.x = 800 * Math.sin(timer + i);
            // sfere.position.z= 500 * Math.sin( timer + i * 1.1 );
            sfere.position.z = 800 * Math.sin(timer + i * 1.1);
        }

        // camera.lookAt(b.position);
        // camera.rotation.y += 0.2 * Math.PI / 180;
        // b.position.z = 25 * Math.cos(theta);
        b6.rotation.y += ADD6;



        renderer6.render(scene6, camera6);
        requestAnimationFrame(mainLoop6);
    };

    init6();
    mainLoop6();
}
   