
git安装 ：https://pan.baidu.com/s/1kU5OCOB#list/path=%2Fpub%2Fgit

什么是版本库呢？版本库又名仓库，英文名repository，你可以简单理解成一个目录，这个目录里面的所有文件都可以被Git管理起来，每个文件的修改、删除，Git都能跟踪，以便任何时刻都可以追踪历史，或者在将来某个时刻可以“还原”。

所以，创建一个版本库非常简单，首先，选择一个合适的地方，创建一个空目录：

$ mkdir learngit
$ cd learngit
$ pwd
/Users/michael/learngit
pwd命令用于显示当前目录。在我的Mac上，这个仓库位于/Users/michael/learngit。

如果你使用Windows系统，为了避免遇到各种莫名其妙的问题，请确保目录名（包括父目录）不包含中文。

第二步，通过git init命令把这个目录变成Git可以管理的仓库：

$ git init
Initialized empty Git repository in /Users/michael/learngit/.git/
瞬间Git就把仓库建好了，而且告诉你是一个空的仓库（empty Git repository），细心的读者可以发现当前目录下多了一个.git的目录，这个目录是Git来跟踪管理版本库的，没事千万不要手动修改这个目录里面的文件，不然改乱了，就把Git仓库给破坏了。

如果你没有看到.git目录，那是因为这个目录默认是隐藏的，用ls -ah命令就可以看见。

言归正传，现在我们编写一个readme.txt文件，内容如下：

Git is a version control system.
Git is free software.
一定要放到learngit目录下（子目录也行），因为这是一个Git仓库，放到其他地方Git再厉害也找不到这个文件。

和把大象放到冰箱需要3步相比，把一个文件放到Git仓库只需要两步。



添加文件：
第一步，用命令git add告诉Git，把文件添加到仓库：

$ git add readme.txt
执行上面的命令，没有任何显示，这就对了，Unix的哲学是“没有消息就是好消息”，说明添加成功。

第二步，用命令git commit告诉Git，把文件提交到仓库：

$ git commit -m "wrote a readme file"
[master (root-commit) cb926e7] wrote a readme file
 1 file changed, 2 insertions(+)
 create mode 100644 readme.txt

0:00

 简单解释一下git commit命令，-m后面输入的是本次提交的说明，可以输入任意内容，当然最好是有意义的，这样你就能从历史记录里方便地找到改动记录。

嫌麻烦不想输入-m "xxx"行不行？确实有办法可以这么干，但是强烈不建议你这么干，因为输入说明对自己对别人阅读都很重要。实在不想输入说明的童鞋请自行Google，我不告诉你这个参数。

git commit命令执行成功后会告诉你，1个文件被改动（我们新添加的readme.txt文件），插入了两行内容（readme.txt有两行内容）。

为什么Git添加文件需要add，commit一共两步呢？因为commit可以一次提交很多文件，所以你可以多次add不同的文件，比如：

$ git add file1.txt
$ git add file2.txt file3.txt
$ git commit -m "add 3 files."


归结：
$ git config --global user.name "殷腊梅"
$ git config --global user.email "1141485216@qq.com"



1、cd 转到文件（建立仓库最好在一个空白文件）
$ cd E:\gitProject

显示当前木兰路
$ pwd



初始化一个Git仓库，使用git init命令。
$  git init


添加文件到Git仓库，分两步：

第一步，使用命令git add <file>，注意，可反复多次使用，添加多个文件；（记住先把文件放在仓库的文件夹下面）
$ git add readme.txt


第二步，使用命令git commit，完成。
$ git commit -m "wrote a readme file"

---------------------------------------下面是记载完整的过程-------------------------------------------------------------------
fineex@DESKTOP-29C67I9 MINGW64 ~
$ git config --global user.name "殷腊梅"

fineex@DESKTOP-29C67I9 MINGW64 ~
$ git config --global user.email "1141485216@qq.com"

fineex@DESKTOP-29C67I9 MINGW64 ~
$ mkdir learngit

fineex@DESKTOP-29C67I9 MINGW64 ~
$ cd learngit

fineex@DESKTOP-29C67I9 MINGW64 ~/learngit
$ pwd
/c/Users/fineex/learngit

fineex@DESKTOP-29C67I9 MINGW64 ~/learngit
$ cd E:\gitProject

fineex@DESKTOP-29C67I9 MINGW64 /e/gitProject
$ pwd
/e/gitProject

fineex@DESKTOP-29C67I9 MINGW64 /e/gitProject
$  git init
Initialized empty Git repository in E:/gitProject/.git/

fineex@DESKTOP-29C67I9 MINGW64 /e/gitProject (master)
$ git add readme.txt
fatal: pathspec 'readme.txt' did not match any files

fineex@DESKTOP-29C67I9 MINGW64 /e/gitProject (master)
$ git add readme.txt

fineex@DESKTOP-29C67I9 MINGW64 /e/gitProject (master)
$ git commit -m "wrote a readme file"
[master (root-commit) 7598555] wrote a readme file
 1 file changed, 59 insertions(+)
 create mode 100644 readme.txt
--------------------------------------------------------------
记住每次提交的方法是：git add filename ,然后是git commit  -m  "注释"
git status 查看当前的状态
git log  查看日志
git log命令显示从最近到最远的提交日志（我们可以看到3次提交，最近的一次是append GPL，上一次是add distributed，最早的一次是wrote a readme file。）
如果嫌输出信息太多，看得眼花缭乱的，可以试试加上--pretty=oneline参数：

