define(['vue'], function (vue) {
    // 定义名为 todo-item 的新组件
    vue.component('todo-item', {
        // todo-item 组件现在接受一个
        // "props"，类似于一个自定义特性。
        // 这个 props 名为 todo。
        //注意此处props是特定的参数不可以随便更改
        props: ['todo'],
        template: '<li>{{ todo.text }}</li>'
    })
    var data = { counter: 0 };

    vue.component('simple-counter', {
        template: '<button v-on:click="counter += 1">{{ counter }}</button>',
        // 技术上 data 的确是一个函数了，因此 Vue 不会警告，
        // 但是我们却给每个组件实例返回了同一个对象的引用
        data: function () {
            return { counter: 0 };
        }
    })

    vue.component('child', {
        // 声明 props
        props: ['message'],
        // 就像 data 一样，prop 也可以在模板中使用
        // 同样也可以在 vm 实例中通过 this.message 来使用
        template: '<li><span>{{ message }}</span></li>'
    })

    var appData = new vue({
        el: "#app",
        data: {  
            isActive: true,
            hasError: false,
            checkedNames: [],
            picked: '',
            selected: '',
            selectedc: [],
            firstname: 'Yin',
            lastName: 'LaMei',
            showmodle:'',
            groceryList: [
                { id: 0, text: '蔬菜' },
                { id: 1, text: '奶酪' },
                { id: 2, text: '随便其它什么人吃的东西' }
            ]
        },
        computed: 
        {
            fullname: {
                get: function () { return firstname + lastName; },
                set: function(newdtr) {
                    firstname = newdtr.splipt(' ')[0];
                    lastName = newdtr.splipt(' ')[1];
                    alert("set");
                }
            },
            familyname: function ()
            {
                return fullname;
            },
            selectdestr:function ()
            {
                return "123ert789opt";
            }

        },
        watch: {
            selectedc: function (newstr, old) {
                alert("old:" + old + ",new:" + newstr);
            },
            showmodle: function (newstr, old) {
                lastName = newstr;
                alert("changge");
            }
        }
    });



    return appData;
})