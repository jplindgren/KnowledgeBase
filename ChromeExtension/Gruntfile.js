module.exports = function(grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        meta: {
          banner:
            '/*!\n' +
            ' * reveal.js <%= pkg.version %> (<%= grunt.template.today("yyyy-mm-dd, HH:MM") %>)\n' +
            ' * http://lab.hakim.se/reveal-js\n' +
            ' * MIT licensed\n' +
            ' *\n' +
            ' * Copyright (C) 2015 Hakim El Hattab, http://hakim.se\n' +
            ' */'
        },
        jshint: {
            files: ['Gruntfile.js', 'src/options.js', 'src/popup.js']
        },
        concat: {
          options: {
            // define a string to put between each file in the concatenated output
            separator: ';'
          },
          popupDist: {
            // the files to concatenate
            src: ['src/autoComplt.js', 'src/popup.js'],
            // the location of the resulting JS file
            dest: 'dist/<%= pkg.name %>.js'
          },
          optionsDist: {
            // the files to concatenate
            src: ['src/options.js', 'src/options-exec.js'],
            // the location of the resulting JS file
            dest: 'dist/options.js'
          }
        },
        uglify: {
            options: {
                // the banner is inserted at the top of the output
                banner: '/*! <%= pkg.name %> <%= grunt.template.today("dd-mm-yyyy") %> */\n'
            },
            dist: {
                files: {
                  'dist/<%= pkg.name %>.min.js': ['<%= concat.popupDist.dest %>'],
                  'dist/options.min.js': ['<%= concat.optionsDist.dest %>']
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
        qunit: {
          all: ['test/**/*.html']
        },
        crx: {
          myPublicPackage: {
            "src": "dist/*",
            "dest": "prod/staging/<%= pkg.name %>-<%= pkg.version %>-dev.crx",
            "zipDest": "prod/production/<%= pkg.name %>-<%= pkg.version %>-dev.zip",
            "options": {
              "maxBuffer": 3000 * 1024 //build extension with a weight up to 3MB
            }
          }
        },
        watch: {
          files: ['<%= jshint.files %>', 'tests/*.js', 'tests/*.html', 'src/*.js'],
          tasks: ['jshint', 'qunit']
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
    grunt.loadNpmTasks('grunt-contrib-qunit');
    grunt.loadNpmTasks('grunt-crx');

    grunt.registerTask('default', ['jshint', 'useminPrepare', 'qunit', 'copy', 'concat', 'uglify', 'usemin', 'crx']);
};