module.exports = function(grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        jshint: {
            files: ['Gruntfile.js', 'src/options.js', 'src/popup.js']
        },
        concat: {
          options: {
            // define a string to put between each file in the concatenated output
            separator: ';'
          },
          dist: {
            // the files to concatenate
            src: ['src/autoComplt.js', 'src/popup.js'],
            // the location of the resulting JS file
            dest: 'dist/<%= pkg.name %>.js'
          }
        },
        uglify: {
            options: {
                // the banner is inserted at the top of the output
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
            },
            dist: {
                files: {
                  'dist/<%= pkg.name %>.min.js': ['<%= concat.dist.dest %>']
                }
            }
        },
        copy: {
          dist: {
            files: [ {src: 'src/popup.html', dest: 'dist/popup.html'}, 
                    {src: 'src/ajax-loader.gif', dest: 'dist/ajax-loader.gif'},
                    {src: 'src/icon.png', dest: 'dist/icon.png'},
                    {src: 'src/manifest.json', dest: 'dist/manifest.json'},
                    {src: 'src/options.html', dest: 'dist/options.html'},
                    {src: 'src/options.js', dest: 'dist/options.js'}
                   ]
          }
        },

        'useminPrepare': {
          options: {
            dest: 'dist'
          },
          html: 'popup.html'
        },

        usemin: {
          html: ['dist/popup.html']
        },
        watch: {
          files: ['<%= jshint.files %>'],
          tasks: ['jshint']
        }
    });

    grunt.registerTask('logvar', function() {
        grunt.log.writeln(grunt.config.get('logvar').data);
    });

    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-usemin');

    grunt.registerTask('default', ['jshint', 'useminPrepare', 'copy', 'concat', 'uglify', 'usemin']);
};